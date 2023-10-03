using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace ModuloAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contato = _context.Contatos.Find(id);
            if (contato == null)
                return NoContent();
            return Ok(contato);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByName(string name)
        {
            var contato = _context.Contatos.Where(x => x.Nome.Equals(name));
            if (contato == null)
                return NoContent();
            return Ok(contato);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllContacts()
        {
            var contato = _context.Contatos;
            if (contato == null)
                return NoContent();
            return Ok(contato);
        }

        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = contato.Id }, contato);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Contato contato)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if (contatoBanco == null)
                return NoContent();

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.Contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);
        }

        [HttpDelete("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            var contatoBanco = _context.Contatos.Find(id);
            if (contatoBanco == null)
                return NoContent();

            _context.Contatos.Remove(contatoBanco);
            _context.SaveChanges();

            return Ok(new { message = "Registro removido do banco" });
        }
    }
}