using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DakiApp.domain.Entities
{
    public class BaseDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set;}

        [Required]
        [DataType(DataType.DateTime)]

        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
    }
}