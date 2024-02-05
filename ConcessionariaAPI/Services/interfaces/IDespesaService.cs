using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Models;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IDespesaService
    {
        Task<Despesa> Create(DespesaDto entity);
        Task<List<Despesa>> GetAll();
        Task<Despesa> GetById(int id);
        Task<Despesa> Update(int id, DespesaDto entity);
        Task Delete(int id);
    }
}
