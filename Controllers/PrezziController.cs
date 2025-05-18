using System;
using System.Threading.Tasks;
using AutoMapper;
using ArticoliWebService.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using ArticoliWebService.Services;
using ArticoliWebService.Models;

namespace ArticoliWebService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/prezzi")]
    public class PrezziController : Controller
    {
        private readonly IPrezziRepository prezziRepository;
        private readonly IMapper mapper;

        public PrezziController (IPrezziRepository prezziRepository, IMapper mapper)
        {
            this.prezziRepository = prezziRepository;
            this.mapper = mapper;
        }

        [HttpGet("{codart}/{idlist?}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PrezziDto))]
        public async Task<IActionResult> GetPriceCodArt(string CodArt, string IdList = null)
        {

            ICollection<DettListini> prezzo = null;
            
            if (IdList != null)
            {
                if (!this.prezziRepository.PrezzoExists(CodArt,IdList))
                {
                    return NotFound();
                }
            }
            

            if (IdList == null)
            {
            prezzo = await this.prezziRepository.SelPrezzoByCodArt(CodArt);
            } 
            else
            {
            prezzo = await this.prezziRepository.SelPrezzoByCodArtAndList(CodArt,IdList);            
            }
            return Ok(mapper.Map<ICollection<PrezziDto>>(prezzo));
        }

        [HttpDelete("elimina/{codart}/{idlist}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePrice(string idlist, string codart)
        {
            if (idlist == "" || codart == "")
            {
                return BadRequest(new InfoMsg(DateTime.Today, $"E' necessario inserire l'id del listino e/o il codice articolo da eliminare!"));
            }

            //verifichiamo che i dati siano stati regolarmente eliminati dal database
            if (!await prezziRepository.DelPrezzoListino(codart,idlist))
            {
                return StatusCode(500, new InfoMsg(DateTime.Today, $"Ci sono stati problemi nella eliminazione del prezzo del codice {codart}.  "));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Eliminazione del prezzo Listino {idlist} del codice {codart} eseguita con successo!"));

        }
    }
}