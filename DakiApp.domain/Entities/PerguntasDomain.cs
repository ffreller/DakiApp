using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

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

        public ICollection<AlternativasDomain> Alternativas { get; set; }
       
    }
}