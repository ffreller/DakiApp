using Microsoft.EntityFrameworkCore;
using DakiApp.domain.Entities;
using System.Linq;
using System;

namespace DakiApp.repository.Context
{
    public class DakiAppContext : DbContext
    {
        public DakiAppContext(DbContextOptions<DakiAppContext> options) : base(options)
        {
            
        }


        public DbSet<AnunciosDomain> Anuncios { get; set; }
        public DbSet<PermissoesDomain> Permissoes { get; set; }
        public DbSet<UsuariosDomain> Usuarios { get; set; }
        public DbSet<PerguntasDomain> Perguntas { get; set; }
        public DbSet<UsuarioPermissoesDomain> UsuarioPermissoes { get; set; }
        public DbSet<RespostasDomain> Respostas { get; set; }
        public DbSet<AlternativasDomain> Alternativas { get; set; }
        public DbSet<QuestionariosDomain> Questionarios { get; set; }
        public DbSet<QuestionarioPerguntasDomain> QuestionarioPerguntas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder){

            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelbuilder.Entity<PerguntasDomain>().ToTable("Perguntas");
            modelbuilder.Entity<AlternativasDomain>().ToTable("Alternativas");
            modelbuilder.Entity<QuestionariosDomain>().ToTable("Questionarios");
            modelbuilder.Entity<QuestionarioPerguntasDomain>().ToTable("QuestionarioPerguntas");
            modelbuilder.Entity<AnunciosDomain>().ToTable("Anuncios");
            modelbuilder.Entity<PermissoesDomain>().ToTable("Permissoes");
            modelbuilder.Entity<RespostasDomain>().ToTable("Respostas");
            modelbuilder.Entity<UsuarioPermissoesDomain>().ToTable("UsuarioPermissoes");
            modelbuilder.Entity<UsuariosDomain>().ToTable("Usuarios");
            
            base.OnModelCreating(modelbuilder);
        }

    }   
}