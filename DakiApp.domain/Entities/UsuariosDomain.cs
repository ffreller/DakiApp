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
        public Endereco Endereco { get; set; }
    }

    public class Endereco
    {
        public string Cidade { get; set; }
        public string Bairro { get; set; }
    }
}