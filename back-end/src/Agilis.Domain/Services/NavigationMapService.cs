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
            var root = new EntidadeNodo(Guid.Empty, "root", "", "times");

            var times = _unitOfWork.TimeRepository.ObterTimes(_usuarioLogado).ToList();
            var timesId = times.Select(t => t.Id);

            var produtos = _unitOfWork.ProdutoRepository.ConsultarTodos(timesId).ToList();
            var produtosId = produtos.Select(p => p.Id);

            foreach (var time in times)
            {
                var timeNodo = new EntidadeNodo(time.Id, time.Nome, "times", "produtos");
                root.AdicionarFilho(timeNodo);

                var produtosDoTime = produtos.Where(p => p.TimeId == time.Id).ToList();
                foreach (var produto in produtosDoTime)
                {
                    var produtoNodo = new EntidadeNodo(produto.Id, produto.Nome, "produtos", "temas");
                    timeNodo.AdicionarFilho(produtoNodo);

                    foreach (var tema in produto.StoryMapping.Temas)
                    {
                        var temaNodo = new EntidadeNodo(tema.Id, tema.Nome, "temas", "epicos");
                        produtoNodo.AdicionarFilho(temaNodo);

                        foreach (var epico in tema.Epicos)
                        {
                            var epicoNodo = new EntidadeNodo(epico.Id, epico.Nome, "epicos", "user-stories");
                            temaNodo.AdicionarFilho(epicoNodo);

                            foreach (var userStory in epico.UserStories)
                            {
                                var userStoryNodo = new EntidadeNodo(userStory.Id, userStory.Nome, "user-stories", "");
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
