using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Trabalho;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Domain.Services.Trabalho
{
    public class ProdutoService : CrudService<Produto>, IProdutoService
    {

        public ProdutoService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.ProdutoRepository)
        {
        }

        public IEnumerable<Produto> ConsultarTodos(IUsuario usuario)
        {
            var timesId = _unitOfWork.TimeRepository.ObterTimes(usuario).Select(t => t.Id).ToList();
            return _unitOfWork.ProdutoRepository.ConsultarTodos(timesId); ;
        }

        public override ICollection<Produto> Pesquisar(string filtro)
          => _unitOfWork.ProdutoRepository
                 .AsQueryable()
                 .Where(t => t.Nome.ToLower().Contains(filtro.ToLower()))
                 .OrderBy(t => t.Nome)
                 .ToList();

        public IEnumerable<Produto> Pesquisar(string filtro, IUsuario usuario)
        {
            var timesId = _unitOfWork.TimeRepository.ObterTimes(usuario).Select(t => t.Id).ToList();

            return _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => timesId.Contains(p.TimeId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public IEnumerable<Produto> Pesquisar(string filtro, Guid timeId, IUsuario usuario)
        {
            var timesId = timeId == Guid.Empty
                ? _unitOfWork.TimeRepository.ObterTimes(usuario).Select(t => t.Id).ToArray()
                : new Guid[] { timeId };

            if (filtro == null)
                filtro = "";

            return _unitOfWork.ProdutoRepository
                    .AsQueryable()
                    .Where(p => timesId.Contains(p.TimeId) && p.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(t => t.Nome)
                    .ToList();
        }

        public override async Task Adicionar(Produto produto)
        {
            //TODO: implementar Renomear de todas as FKs
            await base.Adicionar(produto);
            if (Valid)
            {
                var time = await _unitOfWork.TimeRepository.ConsultarPorId(produto.TimeId);
                var produtoFK = new ProdutoFK(produto.Id, produto.Nome);
                time.AdicionarProduto(produtoFK);
                if (time.Valid)
                    await _unitOfWork.TimeRepository.Atualizar(time);
                else
                    AddNotifications(time);
            }
        }

        public async Task AdicionarTema(Guid produtoId, Tema tema)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
                AddNotification(nameof(produto), "Produto não encontrado");
            else
            {
                produto.StoryMapping.AdicionarTema(tema);
                if (produto.StoryMapping.Invalid)
                    AddNotifications(produto.StoryMapping);
                else
                {
                    await Atualizar(produto);
                    await _unitOfWork.Commit();
                }
            }
        }

        public async Task AdicionarEpico(Guid produtoId, Guid temaId, Epico epico)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
                AddNotification(nameof(produto), "Produto não encontrado");
            else
            {
                var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
                if (tema == null)
                    AddNotification(nameof(tema), "Tema não encontrado");
                else
                {
                    tema.AdicionarEpico(epico);
                    if (tema.Invalid)
                        AddNotifications(tema);
                    else
                    {
                        await Atualizar(produto);
                        await _unitOfWork.Commit();
                    }
                }
            }
        }

        public async Task AdicionarUserStory(Guid produtoId, Guid temaId, Guid epicoId, UserStory userStory)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
                AddNotification(nameof(produto), "Produto não encontrado");
            else
            {
                var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
                if (tema == null)
                    AddNotification(nameof(tema), "Tema não encontrado");
                else
                {
                    var epico = tema.Epicos.FirstOrDefault(e => e.Id == epicoId);
                    if (epico == null)
                        AddNotification(nameof(epico), "Épico não encontrado");
                    else
                    {
                        if (userStory.Invalid)
                            AddNotifications(userStory);
                        else
                        {
                            await _unitOfWork.UserStoryRepository.Adicionar(userStory);

                            var userStoryFK = new UserStoryFK(userStory.Id, userStory.Nome);
                            epico.AdicionarUserStory(userStoryFK);
                            if (epico.Invalid)
                                AddNotifications(epico);
                            else
                            {
                                await Atualizar(produto);
                                await _unitOfWork.Commit();
                            }
                        }
                    }
                }
            }
        }

        public async Task ExcluirTema(Guid produtoId, Guid temaId)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id ==temaId);
            if (tema == null)
            {
                AddNotification(nameof(tema), "Tema não encontrado");
                return;
            }

            produto.StoryMapping.ExcluirTema(tema);
            if (produto.StoryMapping.Invalid)
            {
                AddNotifications(produto.StoryMapping);
                return;
            }
            else
            {
                await Atualizar(produto);
                await _unitOfWork.Commit();
            }
        }

        public async Task MoverUserStory(Guid produtoId, Guid temaId, Guid epicoId, Guid userStoryId, int novaPosicao)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
            if (tema == null)
            {
                AddNotification(nameof(tema), "Tema não encontrado");
                return;
            }

            var epico = tema.Epicos.FirstOrDefault(e => e.Id == epicoId);
            if (epico == null)
            {
                AddNotification(nameof(epico), "Épico não encontrado");
                return;
            }

            epico.MoverUserStory(userStoryId, novaPosicao);
            if (epico.Invalid)
                AddNotifications(epico);
            else
            {
                await Atualizar(produto);
                await _unitOfWork.Commit();
            }
        }

        public async Task RenomearTema(Guid produtoId, Guid temaId, string nome)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
                AddNotification(nameof(produto), "Produto não encontrado");
            else
            {
                var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
                if (tema == null)
                    AddNotification(nameof(tema), "Tema não encontrado");
                else
                {
                    tema.Renomear(nome);
                    if (tema.Invalid)
                        AddNotifications(tema);
                    else
                    {
                        await Atualizar(produto);
                        await _unitOfWork.Commit();
                    }
                }
            }
        }

        public async Task MoverTema(Guid produtoId, Guid temaId, int novaPosicao)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            produto.StoryMapping.MoverTema(temaId, novaPosicao);
            if (produto.StoryMapping.Invalid)
                AddNotifications(produto.StoryMapping);
            else
            {
                await Atualizar(produto);
                await _unitOfWork.Commit();
            }
        }

        public async Task ExcluirEpico(Guid produtoId, Guid temaId, Guid epicoId)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
            if (tema == null)
            {
                AddNotification(nameof(tema), "Tema não encontrado");
                return;
            }

            var epico = tema.Epicos.FirstOrDefault(e => e.Id == epicoId);
            if (epico == null)
            {
                AddNotification(nameof(epico), "Épico não encontrado");
                return;
            }

            //TODO: excluir user stories
            tema.ExcluirEpico(epico);
            if (tema.Invalid)
            {
                AddNotifications(tema);
                return;
            }
            else
            {
                await Atualizar(produto);
                await _unitOfWork.Commit();
            }
        }

        public async Task RenomearEpico(Guid produtoId, Guid temaId, Guid epicoId, string nome)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
                AddNotification(nameof(produto), "Produto não encontrado");
            else
            {
                var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
                if (tema == null)
                    AddNotification(nameof(tema), "Tema não encontrado");
                else
                {
                    var epico = tema.Epicos.FirstOrDefault(e => e.Id == epicoId);
                    if (epico == null)
                        AddNotification(nameof(epico), "Épico não encontrado");
                    else
                    {
                        epico.Renomear(nome);
                        if (epico.Invalid)
                            AddNotifications(epico);
                        else
                        {
                            await Atualizar(produto);
                            await _unitOfWork.Commit();
                        }
                    }
                }
            }
        }

        public async Task MoverEpico(Guid produtoId, Guid temaId, Guid epicoId, int novaPosicao)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
            if (tema == null)
            {
                AddNotification(nameof(tema), "Tema não encontrado");
                return;
            }

            tema.MoverEpico(epicoId, novaPosicao);
            if (tema.Invalid)
                AddNotifications(tema);
            else
            {
                await Atualizar(produto);
                await _unitOfWork.Commit();
            }
        }

        public async Task ExcluirUserStory(Guid produtoId, Guid temaId, Guid epicoId, Guid userStoryId)
        {
            var produto = await ConsultarPorId(produtoId);
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não encontrado");
                return;
            }

            var tema = produto.StoryMapping.Temas.FirstOrDefault(t => t.Id == temaId);
            if (tema == null)
            {
                AddNotification(nameof(tema), "Tema não encontrado");
                return;
            }

            var epico = tema.Epicos.FirstOrDefault(e => e.Id == epicoId);
            if (epico == null)
            {
                AddNotification(nameof(epico), "Épico não encontrado");
                return;
            }

            var userStory = epico.UserStories.FirstOrDefault(us => us.Id == userStoryId);
            if (userStory == null)
            {
                AddNotification(nameof(userStory), "User Story não encontrada");
                return;
            }

            epico.ExcluirUserStory(userStory);
            if (epico.Invalid)
            {
                AddNotifications(epico);
                return;
            }
            else
            {
                await _unitOfWork.UserStoryRepository.Excluir(userStory.Id);
                await Atualizar(produto);
                await _unitOfWork.Commit();
            }
        }
    }
}
