using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DakiApp.domain.Entities
{
    public class PerguntasDomain : BaseDomain
    {
        [Required]
        [StringLength(100)]
        public string Enunciado { get; set; }
        
        [Required]
        [StringLength(2)]
        public string TipoResposta { get; set;}
        
        [Required]
        public bool Obrigatoria { get; set; }

        public ICollection<RespostasDomain> Respostas { get; set; }

        public ICollection<AlternativasDomain> Alternativas { get; set; }

        public ICollection<QuestionarioPerguntasDomain> QuestionarioPerguntas { get; set; }
       
    }
}