using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class QuestionarioPerguntasDomain : BaseDomain
    {
        [ForeignKey("QuestionarioId")]
        public QuestionariosDomain Questionario { get; set; }
        public int QuestionarioId { get; set; }

        [ForeignKey("PerguntaId")]
        public PerguntasDomain Pergunta { get; set; }
        public int PerguntaId { get; set; }
    }
}