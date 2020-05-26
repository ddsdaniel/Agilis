using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Pessoas;
using Agilis.Domain.Models.ValueObjects.Especificacao;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Trabalho
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public Time Time { get; private set; }
        public ICollection<RequisitoNaoFuncional> RequisitosNaoFuncionais { get; private set; }
        public ICollection<Modulo> Modulos { get; private set; }
        public int produckBacklog { get; private set; } // > us > criterios aceitacao
        public LinguagemUbiqua LinguagemUbiqua { get; private set; }

        protected Produto()
        {

        }

        public Produto(string nome,
                       ICollection<RequisitoNaoFuncional> requisitosNaoFuncionais,
                       ICollection<Modulo> modulos,
                       LinguagemUbiqua linguagemUbiqua)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(requisitosNaoFuncionais, nameof(RequisitosNaoFuncionais), "Lista de RNF não pode ser nula")
                .IfNotNull(requisitosNaoFuncionais, c => c.Join(requisitosNaoFuncionais.ToArray()))
                .IsNotNull(modulos, nameof(Modulos), "Lista de Módulos não pode ser nula")
                .IfNotNull(modulos, c => c.Join(modulos.ToArray()))
                .IsNotNull(linguagemUbiqua, nameof(LinguagemUbiqua), "Linguagem Ubíqua não pode ser nula")
                .IfNotNull(linguagemUbiqua, c => c.Join(linguagemUbiqua))
                );

            Nome = nome;
            RequisitosNaoFuncionais = requisitosNaoFuncionais;
            Modulos = modulos;
        }

        public void AdicionarModulo(Modulo modulo)
        {
            if (modulo == null)
            {
                AddNotification(nameof(modulo), "Modulo não pode ser nulo");
                return;
            }

            if (modulo.Invalid)
            {
                AddNotifications(modulo);
                return;
            }

            if (Modulos.Any(r => r.Numero == modulo.Numero))
            {
                AddNotification(nameof(modulo.Numero), $"Já existe um módulo com o número {modulo.Numero}");
                return;
            }

            Modulos.Add(modulo);
        }

        public void RemoverModulo(int numero)
        {
            var modulo = Modulos.FirstOrDefault(r => r.Numero == numero);

            if (modulo == null)
            {
                AddNotification(nameof(numero), "Modulo não encontrado");
                return;
            }

            Modulos.Remove(modulo);
        }

        public void AtualizarNomeModulo(int numero, string nome)
        {
            var modulo = Modulos.FirstOrDefault(r => r.Numero == numero);
            if (modulo == null)
            {
                AddNotification(nameof(numero), "Modulo não encontrado");
                return;
            }

            var indice = Modulos.ToList().FindIndex(r => r.Numero == numero);

            var moduloAtualizado = new Modulo(modulo.Numero, nome, new List<RegraDeNegocio>(), new List<RequisitoFuncional>());

            if (moduloAtualizado.Invalid)
            {
                AddNotifications(moduloAtualizado);
                return;
            }

            Modulos.Remove(modulo);
            Modulos.Add(moduloAtualizado);

            Modulos = Modulos
                .OrderBy(r => r.Numero)
                .ToList();
        }

        public void AdicionarRNF(RequisitoNaoFuncional rnf)
        {
            if (rnf == null)
            {
                AddNotification(nameof(rnf), "RNF não pode ser nulo");
                return;
            }

            if (rnf.Invalid)
            {
                AddNotifications(rnf);
                return;
            }

            if (RequisitosNaoFuncionais.Any(r => r.Numero == rnf.Numero))
            {
                AddNotification(nameof(rnf.Numero), $"Já existe um requisito não funcional com o número {rnf.Numero}");
                return;
            }

            RequisitosNaoFuncionais.Add(rnf);
        }

        public void RemoverRNF(int numero)
        {
            var rnf = RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == numero);

            if (rnf == null)
            {
                AddNotification(nameof(numero), "RNF não encontrado");
                return;
            }

            RequisitosNaoFuncionais.Remove(rnf);
        }

        public void AtualizarDescricaoRnf(int numeroRnf, string descricao)
        {
            var rnf = RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == numeroRnf);
            if (rnf == null)
            {
                AddNotification(nameof(numeroRnf), "RNF não encontrado");
                return;
            }

            var indice = RequisitosNaoFuncionais.ToList().FindIndex(r => r.Numero == numeroRnf);

            var rnfAtualizado = new RequisitoNaoFuncional(rnf.Numero, descricao, rnf.Tipo, rnf.Autor);

            if (rnfAtualizado.Invalid)
            {
                AddNotifications(rnfAtualizado);
                return;
            }

            RequisitosNaoFuncionais.Remove(rnf);
            RequisitosNaoFuncionais.Add(rnfAtualizado);

            RequisitosNaoFuncionais = RequisitosNaoFuncionais
                .OrderBy(r => r.Numero)
                .ToList();
        }

        public void AtualizarTipoRnf(int numeroRnf, TipoRequisitoNaoFuncional tipo)
        {
            var rnf = RequisitosNaoFuncionais.FirstOrDefault(r => r.Numero == numeroRnf);
            if (rnf == null)
            {
                AddNotification(nameof(numeroRnf), "RNF não encontrado");
                return;
            }

            var indice = RequisitosNaoFuncionais.ToList().FindIndex(r => r.Numero == numeroRnf);

            var rnfAtualizado = new RequisitoNaoFuncional(rnf.Numero, rnf.Descricao, tipo, rnf.Autor);

            if (rnfAtualizado.Invalid)
            {
                AddNotifications(rnf);
                return;
            }

            RequisitosNaoFuncionais.Remove(rnf);
            RequisitosNaoFuncionais.Add(rnfAtualizado);

            RequisitosNaoFuncionais = RequisitosNaoFuncionais
                .OrderBy(r => r.Numero)
                .ToList();
        }
    }
}
