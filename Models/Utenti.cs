using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArticoliWebService.Models
{
    public class Utenti
    {
        [Key]
        public string CodFidelity { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Abilitato { get; set; }

        public virtual ICollection<Profili> Profili { get; set; }

    }
}