using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticoliWebService.Models;


namespace ArticoliWebService.Services
{
    public interface IPrezziRepository
    {
        bool PrezzoExists(string CodArt, string IdListino);

        Task<ICollection<DettListini>> SelPrezzoByCodArtAndList(string CodArt , string IdListino);

        Task<ICollection<DettListini>> SelPrezzoByCodArt(string CodArt);
        
        Task<bool> DelPrezzoListino(string CodArt , string IdListino);
    }
    
}