using API.Repositories;
using ModuloAPI.Entities;

namespace API.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task<Contato> CreateAsync(Contato contato)
        {
            return await _contatoRepository.CreateAsync(contato);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _contatoRepository.DeleteAsync(id);
        }

        public async Task<List<Contato>> GetAllContatoAsync()
        {
            return await _contatoRepository.GetAllContatoAsync();
        }

        public async Task<List<Contato>> GetContatoByEmailAsync(string email)
        {
            return await _contatoRepository.GetContatoByEmailAsync(email);
        }

        public async Task<Contato> GetContatoByIdAsync(int id)
        {
            return await _contatoRepository.GetContatoByIdAsync(id);
        }

        public async Task<List<Contato>> GetContatoByNameAsync(string name)
        {
            return await _contatoRepository.GetContatoByNameAsync(name);
        }

        public async Task<Contato> UpdateAsync(int id, Contato contato)
        {
            return await _contatoRepository.UpdateAsync(id, contato);
        }
    }
}
