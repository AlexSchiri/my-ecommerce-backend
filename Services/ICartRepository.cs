using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticoliWebService.Dtos;
using ArticoliWebService.Models;


namespace ArticoliWebService.Services
{
    public interface ICartRepository    
    {
        Task<ICollection<CartDto>> SelCartByUserId(string UserId);  
        Task<ICollection<Cart>> SelCartByUserId2(string UserId);  
        Task<bool> InsCart(Cart cart);
        Task<bool> UpdCart(Cart cart);
        Task<bool> DelAllItemsCart(ICollection<Cart> cart);
        Task<bool> DelSingleItemCart(int id);
        Task<bool> ItemExistsInCart(string codart, string userId);
        int GetIdInCart(string codart, string userId);
        Task<String> GetImageUrl(string Code);
    }
}