using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Core.Domain.Models.Entities;
using Agilis.Application.ViewModels.Produtos;
using System.Threading.Tasks;
using System;
using System.Linq;
using MediatR;

namespace Agilis.Application.Services.Produtos
{
    public class ProdutoCrudAppService
        : CrudAppService<ProdutoViewModel, ProdutoViewModel, Produto>
    {
        public ProdutoCrudAppService(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, unitOfWork.ObterRepository<Produto>(), unitOfWork, mediator)
        {
        }

        public override ProdutoViewModel[] ConsultarTodos()
        {
            return base.ConsultarTodos()
                .OrderBy(p => p.Nome)
                .ToArray();
        }
    }
}
