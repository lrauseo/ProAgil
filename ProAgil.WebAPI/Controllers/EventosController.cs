using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        public IProAgilRepository Repo { get; }
        public IMapper Mapper { get; }

        public EventosController(IProAgilRepository repo, IMapper mapper)
        {
            this.Repo = repo;
            this.Mapper = mapper;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await this.Repo.GetAllEventoAsync(true);
                var results = this.Mapper.Map<IEnumerable<EventoDto>>(eventos);
                return Ok(eventos);
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
                return Ok(this.Mapper.Map<EventoDto>(result));
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
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {                
                var evento = this.Mapper.Map<Evento>(model);
                this.Repo.Add<Evento>(evento);
                if (await this.Repo.SaveChengesAsync())
                    return Created($"api/eventos/{model.Id}", this.Mapper.Map<EventoDto>(evento));
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
            return BadRequest();
        }
        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int eventoId, EventoDto model)
        {
            try
            {
                var evento = await this.Repo.GetAllEventoAsyncById(eventoId, false);
                if (evento == null) return NotFound();
                
                //update do automapper
                this.Mapper.Map(model, evento);
                
                this.Repo.Update<Evento>(evento);

                if (await this.Repo.SaveChengesAsync())
                    return Created($"api/eventos/{model.Id}", this.Mapper.Map<EventoDto>(evento));
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
            return BadRequest();
        }

        [HttpDelete("{EventoId}")]
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