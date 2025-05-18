using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticoliWebService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArticoliWebService.Services
{
    public class ArticoliRepository : IArticoliRepository
    {
        AlphaShopDbContext alphaShopDbContext;

        public ArticoliRepository(AlphaShopDbContext alphaShopDbContext)
        {
            this.alphaShopDbContext =  alphaShopDbContext;
        }

        public async Task<ICollection<Articoli>> SelArticoliByDescrizione(string Descrizione)
        {
            return await this.alphaShopDbContext.Articoli
                .Where(a => a.Descrizione.Contains(Descrizione))
                    .Include(a => a.barcode)
                    .Include(a => a.iva)
                    .Include(a => a.famAssort)
                .OrderBy(a => a.Descrizione)
                .ToListAsync();
        }

        

        public async Task<ICollection<Articoli>> SelArticoliByDescrizione(string Descrizione, string IdCat)
        {
            bool isNumeric = int.TryParse(IdCat, out int n);

            if (string.IsNullOrWhiteSpace(IdCat) || !isNumeric)
            {
                return await this.SelArticoliByDescrizione(Descrizione);
            }

            return await this.alphaShopDbContext.Articoli
                .Where(a => a.Descrizione.Contains(Descrizione))
                .Where(a => a.IdFamAss == int.Parse(IdCat)) 
                    .Include(a => a.barcode)
                    .Include(a => a.famAssort)
                    .Include(a => a.iva)
                .OrderBy(a => a.Descrizione)
                .ToListAsync();
        }

        public async Task<Articoli> SelArticoloByCodice(string Code)
        {
            return await this.alphaShopDbContext.Articoli
                .Where(a => a.CodArt.Equals(Code))
                    .Include(a => a.barcode)
                    .Include(a => a.iva)
                    .Include(a => a.famAssort)
                .FirstOrDefaultAsync();
        }
        public async Task<Articoli> SelArticoloByCodice2(string Code)
        {
            return await this.alphaShopDbContext.Articoli
                .Where(a => a.CodArt.Equals(Code))
                .FirstOrDefaultAsync();
        }
        public async Task<Articoli> SelArticoloByEan(string Ean)
        {
            return await this.alphaShopDbContext.Barcode
                .Where(b => b.Barcode.Equals(Ean))
                    .Include(a => a.articolo.barcode)                                
                    .Include(a => a.articolo.famAssort)
                    .Include(a => a.articolo.iva)
                .Select(a => a.articolo)
                .FirstOrDefaultAsync();       
        }

        public async Task<bool> ArticoloExists(string Code)
        {
            return await this.alphaShopDbContext.Articoli
                .AnyAsync(c => c.CodArt == Code);
        }

        public async Task<bool> InsArticoli(Articoli articolo)
        {
            await this.alphaShopDbContext.AddAsync(articolo);
            return await this.Salva();
        }

        public async Task<bool> UpdArticoli(Articoli articolo)
        {
            this.alphaShopDbContext.Update(articolo);
            return await Salva();
        }

        public async Task<bool> DelArticoli(Articoli articolo)
        {
            this.alphaShopDbContext.Remove(articolo);
            return await Salva();
        }

        private async Task<bool> Salva()
        {
            var saved = await this.alphaShopDbContext.SaveChangesAsync();
            return saved >= 0 ? true : false; 
        }

        public async Task<ICollection<Iva>> SelIva()
        {
            return await this.alphaShopDbContext.Iva
                .OrderBy(a => a.Aliquota)
                .ToListAsync();
        }

        public async Task<ICollection<FamAssort>> SelCat()
        {
            return await this.alphaShopDbContext.Famassort
                .OrderBy(a => a.Id)
                .ToListAsync();
        }

        public async Task<PaginatedResponse<Articoli>> SelArticoli(int pageNumber, int pageSize)
        {
            var totaleArticoli = await this.alphaShopDbContext.Articoli.CountAsync();

            var articoli = await this.alphaShopDbContext.Articoli
                .Include(a => a.barcode)
                .Include(a => a.famAssort)
                .Include(a => a.iva)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<Articoli>
            {
                TotaleArticoli = totaleArticoli,
                PaginaCorrente = pageNumber,
                DimensionePagina = pageSize,
                TotalePagine = (int)Math.Ceiling((double)totaleArticoli / pageSize),
                Articoli = articoli
            };
        }


    }
}