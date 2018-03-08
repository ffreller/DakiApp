using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class AnunciosDomain : BaseDomain
    {
        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(60)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(100)]
        public string Data { get; set; }

        [Required]
        [StringLength(100)]
        public string Contato { get; set; }

        [Required]
        [StringLength(500)]
        public string Descricao { get; set; }

        // [ForeignKey("UsuarioId")]
        // public UsuariosDomain Usuario { get; set; }
        // [Required]
        // public int UsuarioId { get; set; }
    }
}