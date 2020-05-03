using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using Agilis.Domain.Abstractions.Services.Seguranca;
using System;
using System.Linq;
using System.Threading.Tasks;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;

namespace Agilis.Domain.Services.Seguranca
{
    public class UsuarioService : CrudService<Usuario>, IUsuarioService
    {

        public UsuarioService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.UsuarioRepository)
        {
        }

        public override async Task Adicionar(Usuario entity)
        {
            if (entity.Valid)
            {
                var jaExiste = _unitOfWork.UsuarioRepository.ConsultarPorEmail(entity.Email);
                if (jaExiste != null)
                {
                    AddNotification(nameof(Email), "E-mail já cadastrado");
                    return;
                }
            }
            await _unitOfWork.UsuarioRepository.Adicionar(entity);
        }

        public async Task AlterarSenha(Guid id, Email emailLogado, SenhaMedia senhaAtual, SenhaMedia novaSenha, SenhaMedia confirmaNovaSenha)
        {
            var usuario = await ConsultarPorId(id);
            if (usuario == null)
            {
                //TODO: criar uma forma de retornar algo que a Controller possa converter em NotFound: https://trello.com/c/rDy2MBeK
                AddNotification(nameof(id), "Usuário não encontrado");
                return;
            }

            if (usuario.Email.Endereco != emailLogado.Endereco)
            {
                AddNotification(nameof(emailLogado), "Id não corresponde ao usuário logado");
                return;
            }

            usuario.AlterarSenha(senhaAtual, novaSenha, confirmaNovaSenha);
            if (usuario.Invalid)
            {
                AddNotifications(usuario);
                return;
            }

            await _unitOfWork.UsuarioRepository.Atualizar(usuario);
            await Commit();
        }

        public override async Task Atualizar(Usuario entity)
        {
            if (entity.Valid)
            {
                var jaExiste = _unitOfWork.UsuarioRepository.AsQueryable()
                    .Where(u => u.Email.Endereco == entity.Email.Endereco && u.Id != entity.Id)
                    .Any();

                if (jaExiste)
                {
                    AddNotification(nameof(Email), "E-mail já cadastrado");
                    return;
                }
            }
            await _unitOfWork.UsuarioRepository.Atualizar(entity);
        }

        public Usuario Autenticar(Login login)
        {
            if (login.Invalid)
            {
                AddNotifications(login);
                return null;
            }
            else
            {
                var usuario = _unitOfWork.UsuarioRepository.ConsultarPorEmail(login.Email);
                if (usuario == null || usuario.Senha.Conteudo != login.Senha.Conteudo)
                {
                    AddNotification(nameof(Login), "Usuário ou senha incorretos");
                    return null;
                }
                else
                {
                    return usuario;
                }
            }
        }
    }
}
