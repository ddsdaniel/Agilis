using Agilis.Domain.Enums;
using Agilis.Domain.Models.ForeignKeys.Pessoas;
using Agilis.Domain.Models.ForeignKeys.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agilis.Domain.Models.Entities.Pessoas
{
    public class Time : Entity
    {
        public string Nome { get; private set; }
        public EscopoTime Escopo { get; private set; }
        public IEnumerable<UsuarioFK> Colaboradores { get; private set; }
        public IEnumerable<UsuarioFK> Administradores { get; private set; }
        public IEnumerable<ProdutoFK> Produtos { get; private set; }

        protected Time()
        {

        }

        public Time(string nome,
                    EscopoTime escopo,
                    IEnumerable<UsuarioFK> colaboradores,
                    IEnumerable<UsuarioFK> administradores,
                    IEnumerable<ProdutoFK> produtos
                    )
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(colaboradores, nameof(colaboradores), "Lista de colaboradores não deve ser nula")
                .IsNotNull(administradores, nameof(administradores), "Lista de colaboradores não deve ser nula")
                .IsNotNull(produtos, nameof(Produtos), "Lista de produtos não deve ser nula")
                );

            if (administradores != null)
            {
                if (!administradores.Any())
                {
                    AddNotification(nameof(Administradores), "O time deve ter pelo menos um administrador");
                }

                if (escopo == EscopoTime.Pessoal)
                {
                    if (administradores.Count() != 1)
                    {
                        AddNotification(nameof(Administradores), "O time pessoal deve ter um e apenas um administrador");
                    }

                    if (colaboradores.Any())
                    {
                        AddNotification(nameof(Colaboradores), "O time pessoal não deve ter colaboradores");
                    }
                }

                if (colaboradores != null)
                {
                    ValidarColaboradorEAdmin();
                }
            }

            Nome = nome;
            Escopo = escopo;
            Administradores = administradores;
            Colaboradores = colaboradores;
            Produtos = produtos;
        }

        internal void AdicionarAdmin(UsuarioFK admin)
        {
            if (Administradores.Any(a => a.Id == admin.Id))
            {
                AddNotification(nameof(admin), "Admin já adicionado neste time");
                return;
            }

            var novaLista = Administradores.ToList();
            novaLista.Add(admin);

            novaLista = novaLista.OrderBy(a => a.Nome).ToList();

            Administradores = novaLista;

            ValidarColaboradorEAdmin();
        }

        internal void ExcluirAdmin(Usuario admin)
        {
            if (!Administradores.Any(a => a.Id == admin.Id))
                AddNotification(nameof(admin.Id), "Administrador não encontrado");
            else
            {
                Administradores = Administradores
                    .Where(a => a.Id != admin.Id);

                if (Administradores.Count() == 0)
                    AddNotification(nameof(admin.Id), "O time deve ter pelo menos um administrador");
            }
        }

        private void ValidarColaboradorEAdmin()
        {
            if (Administradores != null && Colaboradores != null)
            {
                if (Administradores.Any(a => Colaboradores.Any(c => c.Id == a.Id)) ||
                    Colaboradores.Any(c => Administradores.Any(a => a.Id == c.Id)))
                {
                    AddNotification(nameof(Administradores), "Um usuário deve estar na lista colaboradores ou de administradores");
                }
            }
        }

        internal void AdicionarColaborador(UsuarioFK colab)
        {
            if (Colaboradores.Any(a => a.Id == colab.Id))
            {
                AddNotification(nameof(colab), "Colaborador já adicionado neste time");
                return;
            }

            var novaLista = Colaboradores.ToList();
            novaLista.Add(colab);

            novaLista = novaLista.OrderBy(a => a.Nome).ToList();

            Colaboradores = novaLista;
            ValidarColaboradorEAdmin();
        }

        public void AdicionarProduto(ProdutoFK produto)
        {
            var novaLista = Produtos.ToList();
            novaLista.Add(produto);
            Produtos = novaLista;
        }

        internal void ExcluirColaborador(Usuario colab)
        {
            if (!Colaboradores.Any(a => a.Id == colab.Id))
                AddNotification(nameof(colab.Id), "Colaborador não encontrado");
            else
            {
                Colaboradores = Colaboradores
                    .Where(a => a.Id != colab.Id);
            }
        }

        public override string ToString() => Nome;

        internal void Renomear(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                AddNotification(nameof(Nome), "Nome não deve ser vazio ou nulo");
            else
                Nome = nome;
        }
    }
}
