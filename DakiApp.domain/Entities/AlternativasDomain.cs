using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class AlternativasDomain : BaseDomain
    {
        [Required]
        [StringLength(100)]
        public string Conteudo { get; set; }

        [ForeignKey("PerguntaId")]
        public PerguntasDomain Pergunta { get; set; }
        [Required]
        public int PerguntaId { get; set; }
    }
}