using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Models;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface ITelefoneService
    {
        Task<Telefone> Create(TelefoneDto entity);
        Task<List<Telefone>> GetAll();
        Task<Telefone> GetById(int id);
        Task<Telefone> Update(int id, TelefoneDto entity);
        Task Delete(int id);
    }
}
