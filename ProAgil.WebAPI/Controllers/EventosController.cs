using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        public IProAgilRepository Repo { get; }
        public EventosController(IProAgilRepository repo)
        {
            this.Repo = repo;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await this.Repo.GetAllEventoAsync(true);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet("{EventId}")]
        public async Task<IActionResult> Get(int eventId)
        {
            try
            {
                var result = await this.Repo.GetAllEventoAsyncById(eventId, true);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var result = await this.Repo.GetAllEventoAsyncByTema(tema, true);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                this.Repo.Add(model);
                if (await this.Repo.SaveChengesAsync())
                    return Created($"api/eventos/{model.Id}", model);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
            return BadRequest();
        }
        [HttpPut]
        public async Task<IActionResult> Put(int eventoId, Evento model)
        {
            try
            {
                var evento = await this.Repo.GetAllEventoAsyncById(eventoId, false);
                if (evento == null) return NotFound();

                this.Repo.Update(model);

                if (await this.Repo.SaveChengesAsync())
                    return Created($"api/eventos/{model.Id}", model);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int eventoId)
        {
            try
            {
                var evento = await this.Repo.GetAllEventoAsyncById(eventoId, false);
                if (evento == null) return NotFound();

                this.Repo.Delete(evento);
                
                if (await this.Repo.SaveChengesAsync())
                    return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
            return BadRequest();
        }
    }
}