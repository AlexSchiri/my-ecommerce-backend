namespace ArticoliWebService.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ArticoliWebService.Dtos;
    using ArticoliWebService.Models;
    using ArticoliWebService.Services;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Produces("application/json")]
    [Route("api/iva")]
    public class IvaController : Controller
    {
        private readonly IArticoliRepository articolirepository;
        private readonly IMapper mapper;

        public IvaController(IArticoliRepository articolirepository, IMapper mapper)
        {
            this.articolirepository = articolirepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<IvaDto>))]
        public async Task<ActionResult<IvaDto>> GetIva()
        {
            ICollection<Iva> iva = await this.articolirepository.SelIva();

            return Ok(mapper.Map<ICollection<IvaDto>>(iva));
        }
        
    }
}