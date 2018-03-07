using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class QuestionariosDomain : BaseDomain
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public virtual ICollection<QuestionarioPerguntasDomain> QuestionarioPerguntas { get; set; }

    }
}