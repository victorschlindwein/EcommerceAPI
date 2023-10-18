using API.Services;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("Contato")]
    public class ContatoController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetContatoByIdAsync(int id)
        {
            var contato = await _contatoService.GetContatoByIdAsync(id);

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetContatoByEmailAsync(string email)
        {
            var contato = await _contatoService.GetContatoByEmailAsync(email);

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetContatoByNameAsync(string name)
        {
            var contato = await _contatoService.GetContatoByNameAsync(name);

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllContatoAsync()
        {
            var contato = await _contatoService.GetAllContatoAsync();

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpPost("NewContact")]
        public async Task<IActionResult> CreateAsync(Contato contato)
        {
            contato.DataDeCriacao = DateTime.Now;

            await _contatoService.CreateAsync(contato);

            return CreatedAtAction(nameof(GetContatoByIdAsync), new { id = contato.ContatoId }, contato);
        }

        [HttpPut("EditContact/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, Contato contato)
        {
            var contatoBanco = await _contatoService.UpdateAsync(id, contato);

            return Ok(contatoBanco);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _contatoService.DeleteAsync(id);

            return NoContent();
        }
    }
}