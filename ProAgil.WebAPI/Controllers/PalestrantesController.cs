using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestrantesController : ControllerBase
    {
        public IProAgilRepository Repo { get; }
        public PalestrantesController(IProAgilRepository repo)
        {
            this.Repo = repo;

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await this.Repo.GetPalestrantesAsyncById(id, true);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("getByName/{nome}")]
        public async Task<IActionResult> GetByName(string nome)
        {
            try
            {
                var result = await this.Repo.GetAllPalestrantesAsyncByName(nome, true);
                return Ok(result);
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                this.Repo.Add(model);
                if(await this.Repo.SaveChengesAsync())
                    return Created($"api/palestrentes/{model.Id}", model);
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();

        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Palestrante model)
        {
            try
            {
                var palestrente = await this.Repo.GetPalestrantesAsyncById(id, false);
                if (palestrente == null) return NotFound();

                this.Repo.Update(model);
                if(await this.Repo.SaveChengesAsync())
                    return Created($"api/palestrentes/{model.Id}", model);
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return BadRequest();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var palestrente = await this.Repo.GetPalestrantesAsyncById(id, false);
                if (palestrente == null) return NotFound();

                this.Repo.Delete(palestrente);
                if(await this.Repo.SaveChengesAsync())
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