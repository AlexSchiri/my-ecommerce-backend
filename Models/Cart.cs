using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticoliWebService.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserId {get; set;}
        [Required]
        public string codart {get; set;}
        [Required]
        public int qty {get; set;}
        [Required]
        public Boolean acquistato {get; set;}
        public Articoli Articolo { get; set; }
    }
}