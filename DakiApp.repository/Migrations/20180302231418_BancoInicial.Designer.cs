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
    [Migration("20180302231418_BancoInicial")]
    partial class BancoInicial
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

            modelBuilder.Entity("DakiApp.domain.Entities.PerguntasDomain", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Enunciado")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("TipoResposta")
                        .IsRequired()
                        .HasMaxLength(2);

                    b.HasKey("id");

                    b.ToTable("Perguntas");
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

                    b.Property<int>("UsuarioCriacaoId");

                    b.HasKey("id");

                    b.ToTable("Questionarios");
                });

            modelBuilder.Entity("DakiApp.domain.Entities.AlternativasDomain", b =>
                {
                    b.HasOne("DakiApp.domain.Entities.PerguntasDomain", "Pergunta")
                        .WithMany("Alternativas")
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DakiApp.domain.Entities.QuestionarioPerguntasDomain", b =>
                {
                    b.HasOne("DakiApp.domain.Entities.PerguntasDomain", "Pergunta")
                        .WithMany()
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DakiApp.domain.Entities.QuestionariosDomain", "Questionario")
                        .WithMany()
                        .HasForeignKey("QuestionarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
