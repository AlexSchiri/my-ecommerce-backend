using System.Collections.Generic;
using System.Threading.Tasks;
using ArticoliWebService.Dtos;
using ArticoliWebService.Models;
using ArticoliWebService.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ArticoliWebService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/cat")]
    public class CatController : Controller
    {
        private readonly IArticoliRepository articolirepository;
        private readonly IMapper mapper;

        public CatController(IArticoliRepository articolirepository, IMapper mapper)
        {
            this.articolirepository = articolirepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoriaDto>))]
        public async Task<ActionResult<CategoriaDto>> GetCat()
        {
            ICollection<FamAssort> cat = await this.articolirepository.SelCat();

            return Ok(mapper.Map<ICollection<CategoriaDto>>(cat));

        }
    }
}