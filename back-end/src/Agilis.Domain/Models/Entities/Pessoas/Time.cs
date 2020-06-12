using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Trabalho;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
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
        public IEnumerable<UsuarioVO> Colaboradores { get; private set; }
        public IEnumerable<UsuarioVO> Administradores { get; private set; }
        public IEnumerable<ProdutoVO> Produtos { get; private set; }
        public IEnumerable<ReleaseVO> Releases { get; private set; }

        protected Time()
        {

        }

        public Time(string nome,
                    EscopoTime escopo,
                    IEnumerable<UsuarioVO> colaboradores,
                    IEnumerable<UsuarioVO> administradores,
                    IEnumerable<ReleaseVO> releases,
                    IEnumerable<ProdutoVO> produtos)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(colaboradores, nameof(colaboradores), "Lista de colaboradores não deve ser nula")
                .IfNotNull(colaboradores, c => c.Join(colaboradores.ToArray()))
                .IsNotNull(administradores, nameof(administradores), "Lista de colaboradores não deve ser nula")
                .IfNotNull(administradores, c => c.Join(administradores.ToArray()))
                .IsNotNull(produtos, nameof(produtos), "Lista de Produtos não deve ser nula")
                .IfNotNull(produtos, c => c.Join(produtos.ToArray()))
                .IsNotNull(releases, nameof(releases), "Lista de Releases não deve ser nula")
                .IfNotNull(releases, c => c.Join(releases.ToArray()))                
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
            Releases = releases;
        }

        public void RenomearProduto(Guid id, string nome)
        {
            var produto = Produtos.FirstOrDefault(p => p.Id == id);
            if (produto == null)
                AddNotification(nameof(id), "Produto não encontrado no time");
            else
                produto.Renomear(nome);
        }

        public void RenomearRelease(Guid id, string nome)
        {
            var release = Releases.FirstOrDefault(r => r.Id == id);
            if (release == null)
                AddNotification(nameof(id), "Release não encontrado no time");
            else
                release.Renomear(nome);
        }

        internal void AdicionarAdmin(UsuarioVO admin)
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

        internal void AdicionarColaborador(UsuarioVO colab)
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

        internal void AdicionarProduto(ProdutoVO produto)
        {
            if (Produtos.Any(p => p.Id == produto.Id))
            {
                AddNotification(nameof(produto), "Produto já adicionado neste time");
                return;
            }

            var novaLista = Produtos.ToList();
            novaLista.Add(produto);

            novaLista = novaLista.OrderBy(a => a.Nome).ToList();

            Produtos = novaLista;
        }

        internal void ExcluirProduto(Produto produto)
        {
            if (!Produtos.Any(p => p.Id == produto.Id))
                AddNotification(nameof(produto.Id), "Produto não encontrado");
            else
            {
                Produtos = Produtos.Where(a => a.Id != produto.Id);
            }
        }

        internal void AdicionarRelease(ReleaseVO release)
        {
            if (Releases.Any(r => r.Id == release.Id))
            {
                AddNotification(nameof(release), "Release já adicionada neste time");
                return;
            }

            var novaLista = Releases.ToList();
            novaLista.Add(release);

            novaLista = novaLista.OrderBy(r => r.Ordem).ToList();

            Releases = novaLista;
        }

        internal void ExcluirRelease(Release release)
        {
            if (!Releases.Any(r => r.Id == release.Id))
                AddNotification(nameof(release.Id), "Release não encontrada");
            else
            {
                Releases = Releases.Where(r => r.Id != release.Id);
            }
        }

        public override string ToString() => Nome;
    }
}
