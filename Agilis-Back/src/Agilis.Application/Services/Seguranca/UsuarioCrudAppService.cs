using AutoMapper;
using MediatR;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using Agilis.Infra.Seguranca.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agilis.Application.Services.Seguranca
{
    public class UsuarioCrudAppService
        : CrudAppService<UsuarioCadastroViewModel, UsuarioConsultaViewModel, Usuario>
    {
        private readonly IUnitOfWorkCatalogo _unitOfWork;

        public UsuarioCrudAppService(IUnitOfWorkCatalogo unitOfWork, IMapper mapper, IMediator mediator)
            : base(mapper, mediator, unitOfWork.ObterRepository<Usuario>(), unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<UsuarioConsultaViewModel> AdicionarAsync(UsuarioCadastroViewModel novaEntidadeViewModel)
        {
            var repository = _unitOfWork.ObterRepository<Usuario>();
            if (repository.Consultar().Any(u => u.Email.Endereco == novaEntidadeViewModel.Email))
            {
                AddNotification(nameof(novaEntidadeViewModel.Email), $"Já existe um usuário cadastrado com o e-mail: {novaEntidadeViewModel.Email}");
                return null;
            }
            else if (novaEntidadeViewModel.Senha != novaEntidadeViewModel.ConfirmaSenha)
            {
                AddNotification(nameof(novaEntidadeViewModel.ConfirmaSenha), "Senhas não conferem.");
                return null;
            }

            return await base.AdicionarAsync(novaEntidadeViewModel);
        }

        public override Task AlterarAsync(Guid id, UsuarioCadastroViewModel viewModelCadastro)
        {
            var repository = _unitOfWork.ObterRepository<Usuario>();
            if (repository.Consultar().Any(u => u.Email.Endereco == viewModelCadastro.Email && u.Id != viewModelCadastro.Id))
            {
                AddNotification(nameof(viewModelCadastro.Email), $"Já existe um usuário cadastrado com o e-mail: {viewModelCadastro.Email}");
                return null;
            }

            return base.AlterarAsync(id, viewModelCadastro);
        }

        public override UsuarioConsultaViewModel[] ConsultarTodos()
        {
            return base.ConsultarTodos().OrderBy(p => p.Nome).ToArray();
        }
    }
}
