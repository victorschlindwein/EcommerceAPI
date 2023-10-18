using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace API.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AgendaContext _context;

        public ContatoRepository(AgendaContext context)
        {
            _context = context;
        }

        public async Task<Contato> GetContatoByIdAsync(int contatoId)
        {
            var contato = await _context.Contatos
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.ContatoId == contatoId);

            return contato;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
                return false;

            _context.Contatos.RemoveRange(contato);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Contato>> GetAllContatoAsync()
        {
            var contatos = await _context.Contatos
                .Include(c => c.Enderecos)
                .ToListAsync();

            return contatos;
        }

        public async Task<List<Contato>> GetContatoByEmailAsync(string email)
        {
            var contato = await _context.Contatos.Where(x => x.Email.Equals(email))
                .Include(c => c.Enderecos)
                .ToListAsync();

            return contato;
        }

        public async Task<List<Contato>> GetContatoByNameAsync(string name)
        {
            var contato = await _context.Contatos.Where(x => x.Nome.Equals(name))
                .Include(c => c.Enderecos)
                .ToListAsync();

            return contato;
        }

        public async Task<Contato> CreateAsync(Contato contato)
        {
            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();

            return contato;
        }

        public async Task<Contato> UpdateAsync(int id, Contato contato)
        {
            var contatoBanco = await _context.Contatos.FindAsync(id);

            contatoBanco.Nome = contato.Nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;
            contatoBanco.Email = contato.Email;

            _context.Contatos.Update(contatoBanco);
            await _context.SaveChangesAsync();

            return contatoBanco;
        }
    }
}

