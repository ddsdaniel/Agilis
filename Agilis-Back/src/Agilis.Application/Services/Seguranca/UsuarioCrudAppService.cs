using AutoMapper;
using Agilis.Application.Abstractions.Services;
using Agilis.Application.ViewModels.Seguranca;
using Agilis.Core.Domain.Abstractions.UnitsOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;
using Agilis.Core.Domain.Models.Entities.Seguranca;

namespace Agilis.Application.Services.Seguranca
{
    public class UsuarioCrudAppService
        : CrudAppService<UsuarioCadastroViewModel, UsuarioConsultaViewModel, Usuario>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioCrudAppService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(mapper, unitOfWork.ObterRepository<Usuario>(), unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<UsuarioConsultaViewModel> AdicionarAsync(UsuarioCadastroViewModel novaEntidadeViewModel)
        {
            var repository = _unitOfWork.ObterRepository<Usuario>();
            if (repository.Consultar().Any(u => u.Email.Endereco == novaEntidadeViewModel.Email))
            {
                Criticar($"Já existe um usuário cadastrado com o e-mail: {novaEntidadeViewModel.Email}");
                return null;
            }
            else if (novaEntidadeViewModel.Senha != novaEntidadeViewModel.ConfirmaSenha)
            {
                Criticar("Senhas não conferem.");
                return null;
            }

            return await base.AdicionarAsync(novaEntidadeViewModel);
        }

        public override Task AlterarAsync(Guid id, UsuarioCadastroViewModel viewModelCadastro)
        {
            var repository = _unitOfWork.ObterRepository<Usuario>();
            if (repository.Consultar().Any(u => u.Email.Endereco == viewModelCadastro.Email && u.Id != viewModelCadastro.Id))
            {
                Criticar($"Já existe um usuário cadastrado com o e-mail: {viewModelCadastro.Email}");
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
