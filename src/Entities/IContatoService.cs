using ModuloAPI.Entities;

namespace API.Entities
{
    public interface IContatoService
    {
        Task<Contato> GetById(int id);
        Task<List<Contato>> GetByEmail(string email);
        Task<List<Contato>> GetByName(string name);
        Task<List<Contato>> GetAllContacts();
        Task<Contato> Create(Contato contato);
        Task<Contato> Update(int id, Contato contato);
        Task<bool> Delete(int id);
    }
}
