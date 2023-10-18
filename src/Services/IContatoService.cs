using ModuloAPI.Entities;

namespace API.Services
{
    public interface IContatoService
    {
        Task<Contato> GetContatoByIdAsync(int id);
        Task<List<Contato>> GetContatoByEmailAsync(string email);
        Task<List<Contato>> GetContatoByNameAsync(string name);
        Task<List<Contato>> GetAllContatoAsync();
        Task<Contato> CreateAsync(Contato contato);
        Task<Contato> UpdateAsync(int id, Contato contato);
        Task<bool> DeleteAsync(int id);
    }
}
