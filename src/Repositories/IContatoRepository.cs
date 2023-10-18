using ModuloAPI.Entities;

namespace API.Repositories
{
    public interface IContatoRepository
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
