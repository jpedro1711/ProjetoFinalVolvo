using ConcessionariaAPI.Models;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IProprietarioService
    {
        Task<Proprietario> Create(ProprietarioDto entity);
        Task<List<Proprietario>> GetAll();
        Task<Proprietario> GetById(int id);
        Task<Proprietario> Update(int id, ProprietarioDto entity);
        Task Delete(int id);
    }
}
