using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{   
    public class UsuariosDomain : BaseDomain
    {
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}