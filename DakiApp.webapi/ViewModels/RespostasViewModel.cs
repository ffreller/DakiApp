using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DakiApp.domain.Entities;


namespace DakiApp.webapi.ViewModels
{
    public class RespostasViewModel
    {
        public List<RespostasDomain> Respostas { get; set; }
    }
}