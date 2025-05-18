using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticoliWebService.Models;

namespace ArticoliWebService.Services
{
    public interface IArticoliRepository
    {
        Task<ICollection<Articoli>> SelArticoliByDescrizione(string Descrizione);
        Task<ICollection<Articoli>> SelArticoliByDescrizione(string Descrizione, string IdCat);
        Task<Articoli> SelArticoloByCodice(string Code);
        Task<Articoli> SelArticoloByCodice2(string Code);
        Task<Articoli> SelArticoloByEan(string Ean);
        Task<PaginatedResponse<Articoli>> SelArticoli(int pageNumber, int pageSize);
        Task<bool> InsArticoli(Articoli articolo);
        Task<bool> UpdArticoli(Articoli articolo);
        Task<bool> DelArticoli(Articoli articolo);
        Task<bool> ArticoloExists(string Code);
        Task<ICollection<Iva>> SelIva();
        Task<ICollection<FamAssort>> SelCat();
    }
}