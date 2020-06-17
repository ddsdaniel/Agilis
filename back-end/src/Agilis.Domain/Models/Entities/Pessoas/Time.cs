using Agilis.Domain.Enums;
using Agilis.Domain.Models.Entities.Trabalho;
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
        public IEnumerable<ReleaseFK> Releases { get; private set; }

        protected Time()
        {

        }

        public Time(string nome, UsuarioFK admin)
            : this(nome: nome,
                   escopo: EscopoTime.Colaborativo,
                   colaboradores: new List<UsuarioFK>(),
                   administradores: new List<UsuarioFK> { admin },
                   releases: new List<ReleaseFK>(),
                   produtos: new List<ProdutoFK>()
                   )
        {

        }

        public Time(string nome,
                    EscopoTime escopo,
                    IEnumerable<UsuarioFK> colaboradores,
                    IEnumerable<UsuarioFK> administradores,
                    IEnumerable<ReleaseFK> releases,
                    IEnumerable<ProdutoFK> produtos)
        {
            AddNotifications(new Contract()
                .IsNotNullOrEmpty(nome, nameof(Nome), "Nome inválido")
                .IsNotNull(colaboradores, nameof(colaboradores), "Lista de colaboradores não deve ser nula")
                .IsNotNull(administradores, nameof(administradores), "Lista de colaboradores não deve ser nula")
                .IsNotNull(produtos, nameof(produtos), "Lista de Produtos não deve ser nula")
                .IsNotNull(releases, nameof(releases), "Lista de Releases não deve ser nula")
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
                produto.Nome = nome;
        }

        public void RenomearRelease(Guid id, string nome)
        {
            var release = Releases.FirstOrDefault(r => r.Id == id);
            if (release == null)
                AddNotification(nameof(id), "Release não encontrado no time");
            else
                release.Nome = nome;
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

        internal void AdicionarProduto(ProdutoFK produto)
        {
            if (produto == null)
            {
                AddNotification(nameof(produto), "Produto não deve ser nulo");
                return;
            }

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

        internal void ExcluirProduto(Guid produtoId)
        {
            if (!Produtos.Any(p => p.Id == produtoId))
                AddNotification(nameof(produtoId), "Produto não encontrado");
            else
            {
                Produtos = Produtos.Where(a => a.Id != produtoId);
            }
        }

        internal void AdicionarRelease(ReleaseFK releaseFK)
        {
            if (releaseFK is null)
            {
                AddNotification(nameof(releaseFK), "Release não deve ser nula");
                return;
            }

            if (Releases.Any(r => r.Id == releaseFK.Id))
            {
                AddNotification(nameof(releaseFK), "Release já adicionada neste time");
                return;
            }

            var novaLista = Releases.ToList();
            novaLista.Add(releaseFK);

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

        internal void Renomear(string nome)
        {
            if (String.IsNullOrEmpty(nome))
                AddNotification(nameof(Nome), "Nome não deve ser vazio ou nulo");
            else
                Nome = nome;
        }
    }
}
