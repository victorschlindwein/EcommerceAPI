using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
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
        public async Task<IActionResult> GetById(int id)
        {
            var contato = await _contatoService.GetById(id);

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpGet("GetByEmail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var contato = await _contatoService.GetByEmail(email);

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var contato = await _contatoService.GetByName(name);

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllContacts()
        {
            var contato = await _contatoService.GetAllContacts();

            return contato == null ? NoContent() : Ok(contato);
        }

        [HttpPost("NewContact")]
        public async Task<IActionResult> Create(Contato contato)
        {
            contato.DataDeCriacao = DateTime.Now;

            await _contatoService.Create(contato);

            return CreatedAtAction(nameof(GetById), new { id = contato.ContatoId }, contato);
        }

        [HttpPut("EditContact/{id}")]
        public async Task<IActionResult> Update(int id, Contato contato)
        {
            var contatoBanco = await _contatoService.Update(id, contato);

            return Ok(contatoBanco);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contatoService.Delete(id);
            
            return NoContent();
        }
    }
}