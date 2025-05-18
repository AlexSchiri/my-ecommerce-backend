using System.Collections.Generic;
using System.Threading.Tasks;
using ArticoliWebService.Models;

namespace PriceWebService.Services
{
    public interface IListiniRepository    
    {
        Task<Listini> SelById(string Id);

        Listini SelByIdNoTrack(string Id);

        Listini CheckListino(string Id);
        
        bool InsListini(Listini listino);
        bool UpdListini(Listini listino);
        bool DelListini(Listini listino);
        bool Salva();
    }
}