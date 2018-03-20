using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{   
    public class UsuariosDomain : BaseDomain
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }

        public ICollection<RespostasDomain> Respostas { get; set; }

        public ICollection<AnunciosDomain> Anuncios { get; set; }
        public ICollection<UsuarioPermissoesDomain> UsuarioPermissoes { get; set; }
    }
}