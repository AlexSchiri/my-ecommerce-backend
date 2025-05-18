using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ArticoliWebService.Dtos
{
    public class ArticoliDto
    {
        public string CodArt { get; set; }
        public string Descrizione { get; set; }
        public ICollection<PrezziDto> Prezzi { get; set; } 
        public string Um { get; set; }
        public string CodStat { get; set; }
        public Int16? PzCart { get; set; }
        public double? PesoNetto { get; set; }
        public DateTime? DataCreazione { get; set; }
        public ICollection<BarcodeDto> Ean { get; set; } 
        //public IvaDto Iva { get; set; }
        public string Categoria { get; set; }
        public string IdStatoArt { get; set; }
        public int? IdIva { get; set; }
        public int? IdFamAss { get; set; }
        public string ImageUrl { get; set;}      
        
    }

    public class BarcodeDto
    {
        public string Barcode { get; set; }
        public string Tipo { get; set; }
    }

    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Descrizione { get; set; }
    }

    public class IvaDto
    {
        public IvaDto(int? IdIva, string Descrizione, Int16 Aliquota)
        {
            this.IdIva = IdIva;
            this.Descrizione = Descrizione;
            this.Aliquota = Aliquota;
        }

        public int? IdIva {get; set;}
        public string Descrizione { get; set; }
        public Int16 Aliquota { get; set; }
    }
}