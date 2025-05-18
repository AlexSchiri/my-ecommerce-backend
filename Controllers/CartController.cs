using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArticoliWebService.Dtos;
using ArticoliWebService.Models;
using ArticoliWebService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticoliWebService.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/cart")]
    //[Authorize(Roles = "ADMIN, USER")]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;

        public CartController (ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }

        [HttpGet("cerca/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cart))]
        //[AllowAnonymous]
        public async Task<ActionResult<Articoli>> GetCart(string userId)
        {

            var carrello = await this.cartRepository.SelCartByUserId(userId);        

            if (carrello == null)
            {
                return NotFound(string.Format("Non è stato trovato un carrello per l'utente '{0}'", userId));
            }

            return Ok(carrello);
        }
        

        [HttpPost("inserisci")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Cart))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Articoli>> InsCart([FromBody] Cart cart)
        {
            if (cart == null)
            {
                return BadRequest(new ErrMsg("Dati carrello assenti", this.HttpContext.Response.StatusCode));
            }

            //Verifichiamo che i dati siano corretti
            if (!ModelState.IsValid)
            {
                string ErrVal = "";

                foreach (var modelState in ModelState.Values) 
                {
                    foreach (var modelError in modelState.Errors) 
                    {
                        ErrVal += modelError.ErrorMessage + " - "; 
                    }
                }
                return BadRequest(new ErrMsg(ErrVal, 400));
            }

            cart.acquistato = false;

            var insCart = await cartRepository.InsCart(cart);

            if (!insCart)
            {
                //ModelState.AddModelError("", $"Ci sono stati problemi nell'inserimento dell'Articolo {articolo.CodArt}. ");
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nell'inserimento del carrello.", 500));
            }

            return Ok(new InfoMsg(DateTime.Today, "Inserimento nel carrello eseguito con successo!"));

        }

        [HttpPut("modifica")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Articoli))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        //[Authorize(Roles = "ADMIN")]
        public async Task<ActionResult<Articoli>> UpdateCart([FromBody] Cart cart)
        {
            if (cart == null)
            {
                return BadRequest(new ErrMsg("Dati carrello assenti", this.HttpContext.Response.StatusCode));
            }

            //Verifichiamo che i dati siano corretti
            if (!ModelState.IsValid)
            {
                string ErrVal = "";

                foreach (var modelState in ModelState.Values) 
                {
                    foreach (var modelError in modelState.Errors) 
                    {
                        ErrVal += modelError.ErrorMessage + " - "; 
                    }
                }
                
                return BadRequest(new ErrMsg(ErrVal, 400));
            }

            //Controlliamo se l'articolo è presente in carrello
            var isPresent = await cartRepository.ItemExistsInCart(cart.codart, cart.UserId);
            if (!isPresent)
            {
                var insCart = await cartRepository.InsCart(cart);
                if (!insCart)
                {
                    return StatusCode(500, new ErrMsg($"Ci sono stati problemi nell'inserimento del carrello.", 500));
                }
                return Ok(new InfoMsg(DateTime.Today, "Inserimento nel carrello eseguito con successo!"));
            }
            else
            {
                var id = cartRepository.GetIdInCart(cart.codart,cart.UserId);
                cart.Id = id;
                var updCart = await cartRepository.UpdCart(cart);
                if (!updCart)
                {
                    return StatusCode(500, new ErrMsg($"Ci sono stati problemi nell'aggiornamento del carrello.", 500));
                }
                return Ok(new InfoMsg(DateTime.Today, "Aggiornamento carrello eseguito con successo!"));
            }
        }

        [HttpDelete("elimina/user/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(400, Type = typeof(ErrMsg))]
        [ProducesResponseType(422, Type = typeof(ErrMsg))]
        [ProducesResponseType(500, Type = typeof(ErrMsg))]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteAllItemsCart(string UserId)
        {

            if (UserId == "")
            {
                return BadRequest(new ErrMsg($"E' necessario inserire il codice dell'utente per il carrello da eliminare!",
                    this.HttpContext.Response.StatusCode));
            }

            ICollection<Cart> cart =  await cartRepository.SelCartByUserId2(UserId);

            if (cart == null)
            {
                return StatusCode(422, new ErrMsg($"Non ci sono dati per il carrello dell'utente {UserId}!",
                    this.HttpContext.Response.StatusCode));
            }

            var retVal = await cartRepository.DelAllItemsCart(cart);

            //verifichiamo che i dati siano stati regolarmente eliminati dal database
            if (!retVal)
            {
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nella eliminazione del carrello.",
                    this.HttpContext.Response.StatusCode));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Eliminazione carrello eseguita con successo!"));
        }


        [HttpDelete("elimina/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoMsg))]
        [ProducesResponseType(400, Type = typeof(ErrMsg))]
        [ProducesResponseType(422, Type = typeof(ErrMsg))]
        [ProducesResponseType(500, Type = typeof(ErrMsg))]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteSingleItemCart(int id)
        {

            if (id == 0)
            {
                return BadRequest(new ErrMsg($"E' necessario inserire l'identifier del carrello per eliminarlo!",
                    this.HttpContext.Response.StatusCode));
            }     

            var retVal = await cartRepository.DelSingleItemCart(id);

            //verifichiamo che i dati siano stati regolarmente eliminati dal database
            if (!retVal)
            {
                return StatusCode(500, new ErrMsg($"Ci sono stati problemi nella eliminazione del carrello.",
                    this.HttpContext.Response.StatusCode));
            }

            return Ok(new InfoMsg(DateTime.Today, $"Eliminazione carrello eseguita con successo!"));
        }




        
    }
}