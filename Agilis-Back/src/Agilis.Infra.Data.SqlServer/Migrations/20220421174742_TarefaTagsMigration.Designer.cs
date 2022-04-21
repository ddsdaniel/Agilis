﻿// <auto-generated />
using System;
using Agilis.Infra.Data.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agilis.Infra.Data.SqlServer.Migrations
{
    [DbContext(typeof(AgilisDbContext))]
    [Migration("20220421174742_TarefaTagsMigration")]
    partial class TarefaTagsMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdIntegracaoSac")
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("Nome")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Epico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<Guid?>("ProdutoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Epicos", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EpicoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("EpicoId");

                    b.ToTable("Features", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Produto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UrlRepositorio")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Seguranca.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Seguranca.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Regra")
                        .HasColumnType("int");

                    b.Property<string>("Sobrenome")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataInicial")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Objetivos")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sprints", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Tags.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.ToTable("Tags", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Tarefas.Tarefa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FeatureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RelatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SolucionadorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("RelatorId");

                    b.HasIndex("SolucionadorId");

                    b.ToTable("Tarefas", (string)null);
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Time", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.ToTable("Times", (string)null);
                });

            modelBuilder.Entity("TagTarefa", b =>
                {
                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TarefasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TagsId", "TarefasId");

                    b.HasIndex("TarefasId");

                    b.ToTable("TagTarefa");
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Epico", b =>
                {
                    b.HasOne("Agilis.Core.Domain.Models.Entities.Produto", "Produto")
                        .WithMany("Epicos")
                        .HasForeignKey("ProdutoId");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Feature", b =>
                {
                    b.HasOne("Agilis.Core.Domain.Models.Entities.Epico", "Epico")
                        .WithMany("Features")
                        .HasForeignKey("EpicoId");

                    b.Navigation("Epico");
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Seguranca.Usuario", b =>
                {
                    b.OwnsOne("Agilis.Core.Domain.Models.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Endereco")
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Email");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.OwnsOne("Agilis.Core.Domain.Models.ValueObjects.Seguranca.Senha", "Senha", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Conteudo")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("nvarchar(256)")
                                .HasColumnName("Senha");

                            b1.HasKey("UsuarioId");

                            b1.ToTable("Usuarios");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Email");

                    b.Navigation("Senha");
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Tags.Tag", b =>
                {
                    b.OwnsOne("Agilis.Core.Domain.Models.ValueObjects.HtmlColor", "Cor", b1 =>
                        {
                            b1.Property<Guid>("TagId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Codigo")
                                .HasMaxLength(7)
                                .HasColumnType("nvarchar(7)")
                                .HasColumnName("Cor");

                            b1.HasKey("TagId");

                            b1.ToTable("Tags");

                            b1.WithOwner()
                                .HasForeignKey("TagId");
                        });

                    b.Navigation("Cor");
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Tarefas.Tarefa", b =>
                {
                    b.HasOne("Agilis.Core.Domain.Models.Entities.Feature", "Feature")
                        .WithMany("Tarefas")
                        .HasForeignKey("FeatureId");

                    b.HasOne("Agilis.Core.Domain.Models.Entities.Seguranca.Usuario", "Relator")
                        .WithMany()
                        .HasForeignKey("RelatorId");

                    b.HasOne("Agilis.Core.Domain.Models.Entities.Seguranca.Usuario", "Solucionador")
                        .WithMany()
                        .HasForeignKey("SolucionadorId");

                    b.OwnsOne("Agilis.Core.Domain.Models.ValueObjects.Hora", "HorasPrevistas", b1 =>
                        {
                            b1.Property<Guid>("TarefaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Horario")
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)")
                                .HasColumnName("HorasPrevistas");

                            b1.HasKey("TarefaId");

                            b1.ToTable("Tarefas");

                            b1.WithOwner()
                                .HasForeignKey("TarefaId");
                        });

                    b.OwnsOne("Agilis.Core.Domain.Models.ValueObjects.Hora", "HorasRealizadas", b1 =>
                        {
                            b1.Property<Guid>("TarefaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Horario")
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)")
                                .HasColumnName("HorasRealizadas");

                            b1.HasKey("TarefaId");

                            b1.ToTable("Tarefas");

                            b1.WithOwner()
                                .HasForeignKey("TarefaId");
                        });

                    b.Navigation("Feature");

                    b.Navigation("HorasPrevistas");

                    b.Navigation("HorasRealizadas");

                    b.Navigation("Relator");

                    b.Navigation("Solucionador");
                });

            modelBuilder.Entity("TagTarefa", b =>
                {
                    b.HasOne("Agilis.Core.Domain.Models.Entities.Tags.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agilis.Core.Domain.Models.Entities.Tarefas.Tarefa", null)
                        .WithMany()
                        .HasForeignKey("TarefasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Epico", b =>
                {
                    b.Navigation("Features");
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Feature", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("Agilis.Core.Domain.Models.Entities.Produto", b =>
                {
                    b.Navigation("Epicos");
                });
#pragma warning restore 612, 618
        }
    }
}
