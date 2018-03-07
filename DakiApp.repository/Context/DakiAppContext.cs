using Microsoft.EntityFrameworkCore;
using DakiApp.domain.Entities;

namespace DakiApp.repository.Context
{
    public class DakiAppContext : DbContext
    {
        public DakiAppContext(DbContextOptions<DakiAppContext> options) : base(options)
        {
            
        }

        public DbSet<PerguntasDomain> Perguntas { get; set; }

        public DbSet<AlternativasDomain> Alternativas { get; set; }

        public DbSet<QuestionariosDomain> Questionarios { get; set; }

        public DbSet<QuestionarioPerguntasDomain> QuestionarioPerguntas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder){
            modelbuilder.Entity<PerguntasDomain>().ToTable("Perguntas");
            modelbuilder.Entity<AlternativasDomain>().ToTable("Alternativas");
            modelbuilder.Entity<QuestionariosDomain>().ToTable("Questionarios");
            modelbuilder.Entity<QuestionarioPerguntasDomain>().ToTable("QuestionarioPerguntas");
            
            base.OnModelCreating(modelbuilder);
        }
    }   
}