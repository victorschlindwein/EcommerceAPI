using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using ModuloAPI.Entities;

namespace API.Services
{
    public class ContatoService : IContatoService
    {
        private readonly AgendaContext _context;

        public ContatoService(AgendaContext context)
        {
            _context = context;
        }

        public async Task<Contato> GetById(int contatoId)
        {
            var contato = await _context.Contatos
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.ContatoId == contatoId);
            return contato;
        }

        public async Task<bool> Delete(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null)
                return false;
            _context.Contatos.RemoveRange(contato);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Contato>> GetAllContacts()
        {
            var contatos = await _context.Contatos
                .Include(c => c.Enderecos)
                .ToListAsync();

            return contatos;
        }

        public async Task<List<Contato>> GetByEmail(string email)
        {
            var contato = await _context.Contatos.Where(x => x.Email.Equals(email))
                .Include(c => c.Enderecos)
                .ToListAsync();

            return contato;
        }

        public async Task<List<Contato>> GetByName(string name)
        {
            var contato = await _context.Contatos.Where(x => x.Nome.Equals(name))
                .Include(c => c.Enderecos)
                .ToListAsync();

            return contato;
        }

        public async Task<Contato> Create(Contato contato)
        {
            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();

            return contato;
        }

        public async Task<Contato> Update(int id, Contato contato)
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
