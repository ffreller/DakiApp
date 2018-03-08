using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class PermissoesDomain : BaseDomain
    {
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
    }
}