using System.Collections.Generic;

namespace ArticoliWebService.Models
{

    public class PaginatedResponse<T>
    {
        public int TotaleArticoli { get; set; }
        public int PaginaCorrente { get; set; }
        public int DimensionePagina { get; set; }
        public int TotalePagine { get; set; }
        public List<T> Articoli { get; set; } // Utilizza una proprietà generica per i dati
    }

}