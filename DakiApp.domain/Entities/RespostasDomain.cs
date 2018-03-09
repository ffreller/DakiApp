using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class RespostasDomain : BaseDomain
    {
        public string Texto { get; set; }
              
        [ForeignKey("AlternativaId")]
        public AlternativasDomain Alternativa { get; set; }
        public int? AlternativaId { get; set; }

        [ForeignKey("PerguntaId")]
        public PerguntasDomain Pergunta { get; set; }
        public int PerguntaId { get; set; }

        [ForeignKey("UsuarioId")]
        public UsuariosDomain Usuario { get; set; }
        [Required]
        public int UsuarioId { get; set; }


    }
}