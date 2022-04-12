﻿using Agilis.Core.Domain.Models.Entities;
using Agilis.Core.Domain.Models.Entities.Seguranca;
using DDS.Validacoes.Abstractions.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Agilis.Infra.Data.SqlServer
{
    public class AgilisDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Time> Times { get; set; }

        public AgilisDbContext(DbContextOptions<AgilisDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly assemblyWithConfigurations = GetType().Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);

            modelBuilder.Ignore<Validavel>();
        }
    }
}
