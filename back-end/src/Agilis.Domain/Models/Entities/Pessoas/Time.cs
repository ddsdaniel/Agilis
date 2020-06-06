using Agilis.Domain.Enums;
using Agilis.Domain.Models.ValueObjects.Pessoas;
using Agilis.Domain.Models.ValueObjects.Trabalho;
using DDS.Domain.Core.Abstractions.Model.Entities;
using Flunt.Validations;
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

        protected Time()
        {

        }

        public Time(string nome,
                    EscopoTime escopo,
                    IEnumerable<UsuarioVO> colaboradores,
                    IEnumerable<UsuarioVO> administradores,
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
            }

            Nome = nome;
            Escopo = escopo;
            Administradores = administradores;
            Colaboradores = colaboradores;
            Produtos = produtos;
        }
    }
}
