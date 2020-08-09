using DDS.Domain.Core.Model.ValueObjects;
using DDS.Domain.Core.Model.ValueObjects.Seguranca.Senhas;
using Agilis.Domain.Abstractions.Repositories;
using Agilis.Domain.Abstractions.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Seguranca;
using Agilis.Domain.Abstractions.Services.Pessoas;
using System.Collections.Generic;
using Agilis.Domain.Enums;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;

namespace Agilis.Domain.Services.Pessoas
{
    public class UsuarioService : CrudService<Usuario>, IUsuarioService
    {

        public UsuarioService(IUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.UsuarioRepository)
        {
        }

        public override async Task Adicionar(Usuario usuario)
        {
            if (usuario.Valid)
            {
                var jaExiste = _unitOfWork.UsuarioRepository.ConsultarPorEmail(usuario.Email);
                if (jaExiste != null)
                {
                    AddNotification(nameof(Email), "E-mail já cadastrado");
                    return;
                }
            }
            await _unitOfWork.UsuarioRepository.Adicionar(usuario);

            var timePessoal = new Time("Pessoal",
                                       EscopoTime.Pessoal,
                                       new List<UsuarioFK>(),
                                       new List<UsuarioFK> { new UsuarioFK(usuario.Id, usuario.Nome, usuario.Email.Endereco) },
                                       new List<ProdutoFK>()
                                       );
            await _unitOfWork.TimeRepository.Adicionar(timePessoal);
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

        public Usuario ConsultarPorEmail(Email email) => _unitOfWork.UsuarioRepository.ConsultarPorEmail(email);

        public override ICollection<Usuario> Pesquisar(string filtro)
             => _unitOfWork.UsuarioRepository
                    .AsQueryable()
                    .Where(u => u.Nome.ToLower().Contains(filtro.ToLower()))
                    .OrderBy(u => u.Nome)
                    .ToList();
        
    }
}
