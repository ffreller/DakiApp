using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DakiApp.domain.Entities
{
    public class UsuarioPermissoes  : BaseDomain
    {
        [ForeignKey("UsuarioId")]
        public UsuariosDomain Usuario { get; set; }
        public int UsuarioId { get; set; }

        [ForeignKey("PermissaoId")]
        public PermissoesDomain Permissao { get; set; }
        public int PermissaoId { get; set; }
    }
    }
}