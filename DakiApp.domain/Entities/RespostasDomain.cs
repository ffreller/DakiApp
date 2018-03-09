using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class RespostasDomain : BaseDomain
    {
        public string Resposta { get; set; }
        
        public ICollection<AlternativasDomain> Usuarios { get; set; }
        
        [ForeignKey("AlternativaId")]
        public QuestionariosDomain Alternativa { get; set; }
        public int AlternativaId { get; set; }

        [ForeignKey("PerguntaId")]
        public PerguntasDomain Pergunta { get; set; }
        public int PerguntaId { get; set; }


    }
}