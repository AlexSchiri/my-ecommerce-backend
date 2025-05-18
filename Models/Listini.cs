using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArticoliWebService.Models
{
    public class Listini
    {
        [Key]
        public string Id {get; set;}
        [MinLength( 5, ErrorMessage = "La descrizione dever avere almeno 5 caratteri")]
        [MaxLength( 5, ErrorMessage = "La descrizione dever avere max 30 caratteri")]
        public string Descrizione {get; set;}

        public string Obsoleto {get; set;}

        public virtual ICollection<DettListini> DettListini { get; set; }

    }
}