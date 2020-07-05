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
            var root = new EntidadeNodo(Guid.Empty, "root");

            var times = _unitOfWork.TimeRepository.ObterTimes(_usuarioLogado);
            var produtos = _unitOfWork.ProdutoRepository.ConsultarTodos(_usuarioLogado);

            foreach (var time in times)
            {
                var timeNodo = new EntidadeNodo(time.Id, time.Nome);
                root.AdicionarFilho(timeNodo);

                var produtosDoTime = produtos.Where(p => p.TimeId == time.Id).ToList();
                foreach (var produto in produtosDoTime)
                {
                    var produtoNodo = new EntidadeNodo(produto.Id, produto.Nome);
                    timeNodo.AdicionarFilho(produtoNodo);
                }
            }

            return root;
        }
    }
}
