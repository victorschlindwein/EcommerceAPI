using API.Entities;
using Microsoft.AspNetCore.Mvc;
using ModuloAPI.Context;

namespace API.Controllers
{
    [ApiController]
    [Route("Endereco")]
    public class EnderecoController : Controller
    {
        private readonly AgendaContext _context;

        public EnderecoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost("NewAddressToContact/{ContatoId}")]
        public async Task<IActionResult> CreateNewAddress(int ContatoId, Endereco endereco)
        {
            endereco.ContatoId = ContatoId;

            var contatoBanco = await _context.Contatos.FindAsync(endereco.ContatoId);
            if (contatoBanco == null)
                return NoContent();

            await _context.Enderecos.AddAsync(endereco);
            await _context.SaveChangesAsync();

            return endereco == null ? NoContent() : Ok(endereco);
        }
    }
}
