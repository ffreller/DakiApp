﻿// <auto-generated />
using DakiApp.repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DakiApp.repository.Migrations
{
    [DbContext(typeof(DakiAppContext))]
    [Migration("20180315222354_Obrigatoria")]
    partial class Obrigatoria
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DakiApp.domain.Entities.AlternativasDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<int>("PerguntaId");

                    b.HasKey("id");

                    b.HasIndex("PerguntaId");

                    b.ToTable("Alternativas");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.AnunciosDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contato")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("UsuarioId");

                    b.HasKey("id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Anúncios");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.PerguntasDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Enunciado")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("Obrigatoria");

                    b.Property<string>("TipoResposta")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("id");

                    b.ToTable("Perguntas");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.PermissoesDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("id");

                    b.ToTable("Permissões");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.QuestionarioPerguntasDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<int>("PerguntaId");

                    b.Property<int>("QuestionarioId");

                    b.HasKey("id");

                    b.HasIndex("PerguntaId");

                    b.HasIndex("QuestionarioId");

                    b.ToTable("QuestionarioPerguntas");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.QuestionariosDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("id");

                    b.ToTable("Questionarios");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.RespostasDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AlternativaId");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<int>("PerguntaId");

                    b.Property<string>("Texto");

                    b.Property<int>("UsuarioId");

                    b.HasKey("id");

                    b.HasIndex("AlternativaId");

                    b.HasIndex("PerguntaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Respostas");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.UsuarioPermissoesDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<int>("PermissaoId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("id");

                    b.HasIndex("PermissaoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("UsuarioPermissoes");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.UsuariosDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Email");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.HasKey("id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.AlternativasDomain", b =>
                {
                    b.HasOne("DakiApp.domain.Entities.PerguntasDomain", "Pergunta")
                        .WithMany("Alternativas")
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DakiApp.domain.Entities.AnunciosDomain", b =>
                {
                    b.HasOne("DakiApp.domain.Entities.UsuariosDomain", "Usuario")
                        .WithMany("Anuncios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DakiApp.domain.Entities.QuestionarioPerguntasDomain", b =>
                {
                    b.HasOne("DakiApp.domain.Entities.PerguntasDomain", "Pergunta")
                        .WithMany()
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DakiApp.domain.Entities.QuestionariosDomain", "Questionario")
                        .WithMany("QuestionarioPerguntas")
                        .HasForeignKey("QuestionarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DakiApp.domain.Entities.RespostasDomain", b =>
                {
                    b.HasOne("DakiApp.domain.Entities.AlternativasDomain", "Alternativa")
                        .WithMany()
                        .HasForeignKey("AlternativaId");

                    b.HasOne("DakiApp.domain.Entities.PerguntasDomain", "Pergunta")
                        .WithMany()
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DakiApp.domain.Entities.UsuariosDomain", "Usuario")
                        .WithMany("Respostas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DakiApp.domain.Entities.UsuarioPermissoesDomain", b =>
                {
                    b.HasOne("DakiApp.domain.Entities.PermissoesDomain", "Permissao")
                        .WithMany()
                        .HasForeignKey("PermissaoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DakiApp.domain.Entities.UsuariosDomain", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
