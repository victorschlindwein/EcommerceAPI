using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contato =  await _context.Contatos.FindAsync(id);
            if (contato == null)
                return NoContent();
            return Ok(contato);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var contato = await _context.Contatos.Where(x => x.Nome.Equals(name)).ToListAsync();
            if (contato == null)
                return NoContent();
            return Ok(contato);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllContacts()
        {
            List<Contato> contato = await _context.Contatos.ToListAsync();
            if (contato == null)
                return NoContent();
            return Ok(contato);
        }

        [HttpPost("NewContact")]
        public async Task<IActionResult> Create(Contato contato)
        {
            await _context.AddAsync(contato);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = contato.Id }, contato);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Update(int id, Contato contato)
        {
            var contatoBanco = await _context.Contatos.FindAsync(id);
            if (contatoBanco == null)
                return NoContent();

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            await _context.SaveChangesAsync();

            return Ok(contatoBanco);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if (contatoBanco == null)
                return NoContent();

            _context.Contatos.Remove(contatoBanco);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registro removido do banco" });
        }
    }
}