using Agilis.Domain.Abstractions.Entities;
using Agilis.Domain.Abstractions.Entities.Pessoas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using DDS.Domain.Core.Abstractions.Services;
using System;
using System.Linq;

namespace Agilis.Domain.Services
{
    public class NavigationMapService : Service, INavigationMapService
    {
        private readonly IUsuario _usuarioLogado;
        private readonly IUnitOfWork _unitOfWork;

        public NavigationMapService(IUsuario usuarioLogado,
                                    IUnitOfWork unitOfWork)
        {
            _usuarioLogado = usuarioLogado;
            _unitOfWork = unitOfWork;
        }

        public EntidadeNodo Obter()
        {
            var root = new EntidadeNodo(Guid.Empty, "root", "");

            var times = _unitOfWork.TimeRepository.ObterTimes(_usuarioLogado).ToList();
            var timesId = times.Select(t => t.Id);

            var produtos = _unitOfWork.ProdutoRepository.ConsultarTodos(timesId).ToList();
            var produtosId = produtos.Select(p => p.Id);

            var temas = _unitOfWork.TemaRepository.ConsultarTodos(produtosId).ToList();
            var temasId = temas.Select(t => t.Id);

            var epicos = _unitOfWork.EpicoRepository.ConsultarTodos(temasId).ToList();
            var epicosId = epicos.Select(e => e.Id);

            var userStories = _unitOfWork.UserStoryRepository.ConsultarTodas(epicosId).ToList();
            var userStoriesId = userStories.Select(us => us.Id);

            foreach (var time in times)
            {
                var timeNodo = new EntidadeNodo(time.Id, time.Nome, "times");
                root.AdicionarFilho(timeNodo);

                var produtosDoTime = produtos.Where(p => p.TimeId == time.Id).ToList();
                foreach (var produto in produtosDoTime)
                {
                    var produtoNodo = new EntidadeNodo(produto.Id, produto.Nome, "produtos");
                    timeNodo.AdicionarFilho(produtoNodo);

                    var temasDoProduto = temas.Where(t => t.ProdutoId == produto.Id).ToList();
                    foreach (var tema in temasDoProduto)
                    {
                        var temaNodo = new EntidadeNodo(tema.Id, tema.Nome, "temas");
                        produtoNodo.AdicionarFilho(temaNodo);

                        var epicosDoTema = epicos.Where(e => e.TemaId == tema.Id).ToList();
                        foreach (var epico in epicosDoTema)
                        {
                            var epicoNodo = new EntidadeNodo(epico.Id, epico.Nome, "epicos");
                            temaNodo.AdicionarFilho(epicoNodo);

                            var userStoriesDoEpico = userStories.Where(us => us.EpicoId == epico.Id);
                            foreach (var userStory in userStoriesDoEpico)
                            {
                                var userStoryNodo = new EntidadeNodo(userStory.Id, userStory.Historia, "user-stories");
                                epicoNodo.AdicionarFilho(userStoryNodo);
                            }
                        }
                    }
                }
            }

            return root;
        }
    }
}
