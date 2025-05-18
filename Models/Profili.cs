using System.ComponentModel.DataAnnotations;

namespace ArticoliWebService.Models
{
    public class Profili
    {
        [Key]
        public int Id { get; set; }
        public string CodFidelity { get; set; }
        public string Tipo { get; set; }

        public virtual Utenti Utente { get; set; } 
    }
}