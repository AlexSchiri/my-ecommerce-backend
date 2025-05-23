using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticoliWebService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace ArticoliWebService.Services
{
    public class PrezziRepository : IPrezziRepository
    {
        private AlphaShopDbContext alphaShopDbContext;

        public PrezziRepository(AlphaShopDbContext alphaShopDbContext)
        {
            this.alphaShopDbContext = alphaShopDbContext;
        }

        public async Task<ICollection<DettListini>> SelPrezzoByCodArtAndList(string CodArt, string IdListino)
        {
            return await alphaShopDbContext.DettListini
                .Where(a => a.CodArt.Equals(CodArt) && a.IdList.Equals(IdListino))
                .ToListAsync();
        }

        public bool PrezzoExists(string CodArt, string IdListino)
        {
            return this.alphaShopDbContext.DettListini
                .Any(a => a.CodArt.Equals(CodArt) && a.IdList.Equals(IdListino));
        }

        public async Task<bool> DelPrezzoListino(string CodArt, string IdListino)
        {
            var Sql = "DELETE FROM DETTLISTINI WHERE CodArt = @CodArt AND IdList = @IdList";
            var parCode = new SqlParameter("@CodArt ", CodArt);
            var parList = new SqlParameter("@IdList", IdListino);

            int righe = await this.alphaShopDbContext
                .Database.ExecuteSqlRawAsync(Sql, parCode, parList);

            return (righe > 0) ? true : false; 
        }

        public async Task<ICollection<DettListini>> SelPrezzoByCodArt(string CodArt)
        {
            return await alphaShopDbContext.DettListini
                .Where(a => a.CodArt.Equals(CodArt))
                .ToListAsync();
        }

        
    }
}
