using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ArticoliWebService.Dtos;
using ArticoliWebService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace ArticoliWebService.Services
{
    public class CartRepository : ICartRepository
    {
        private AlphaShopDbContext alphaShopDbContext;

        public CartRepository(AlphaShopDbContext alphaShopDbContext)
        {
            this.alphaShopDbContext = alphaShopDbContext;
        }

        public async Task<bool> DelAllItemsCart(ICollection<Cart> cart)
        {
            this.alphaShopDbContext.RemoveRange(cart);
            return await Salva();    
        }

        public async Task<bool> DelSingleItemCart(int id)
        {
            var cart =  this.alphaShopDbContext.Cart.Where(a => a.Id == id && a.acquistato == false);
            this.alphaShopDbContext.RemoveRange(cart);
            return await Salva();
        }

        public int GetIdInCart(string codart, string userId)
        {
            int id =  alphaShopDbContext.Cart.Where(u => u.codart == codart && u.UserId == userId && u.acquistato == false)
            .Select(u => u.Id)
            .SingleOrDefault();
            return id;
        }

        public async Task<string> GetImageUrl(string codart)
        {
            return await alphaShopDbContext.Articoli.Where(p =>p.CodArt == codart)
            .Select(p => p.ImageUrl).SingleOrDefaultAsync();
 
        }

        public async Task<bool> InsCart(Cart cart)
        {
            var cartFound = await this.ItemExistsInCart(cart.codart, cart.UserId);
            if (cartFound == false) {
                await this.alphaShopDbContext.AddAsync(cart);
                return await this.Salva();
            } else {
                var cartItem = await alphaShopDbContext.Cart.Where(u => u.codart == cart.codart 
                                                    && u.UserId == cart.UserId 
                                                    && u.acquistato == false)
                                                    .SingleOrDefaultAsync();
                cartItem.qty += cart.qty;
                await this.UpdCart(cartItem);
                return await this.Salva();
            };
        }

        public async Task<bool> ItemExistsInCart(string codart, string userId)
        {
            return await this.alphaShopDbContext.Cart
                .AnyAsync(c => c.codart == codart && c.UserId == userId && c.acquistato == false);
        }

        public async Task<ICollection<CartDto>> SelCartByUserId(string UserId)
        {
            var cart = await this.alphaShopDbContext.Cart
                .Where(a => a.UserId == UserId)
                .Where(a => a.acquistato == false)
                .Select(c => new CartDto{
                    Id =c.Id,
                    codart = c.codart,
                    UserId = c.UserId,
                    acquistato = c.acquistato,
                    qty = c.qty,
                    ImageUrl = c.Articolo.ImageUrl
                })
                //.Include(c => c.Articolo) //include recuoera tutta l'entità, non la singola proprietà
                .ToListAsync();
            return cart;
        }

                public async Task<ICollection<Cart>> SelCartByUserId2(string UserId)
        {
            return  await this.alphaShopDbContext.Cart
                .Where(a => a.UserId == UserId)
                .Where(a => a.acquistato == false)
                //.Include(c => c.Articolo) //include recuoera tutta l'entità, non la singola proprietà
                .ToListAsync();
        }

        

        public async Task<bool> UpdCart(Cart cart)
        {
            this.alphaShopDbContext.Update(cart);
            return await Salva();
        }

        private async Task<bool> Salva()
        {
            var saved = await this.alphaShopDbContext.SaveChangesAsync();
            return saved >= 0 ? true : false; 
        }
    }

}