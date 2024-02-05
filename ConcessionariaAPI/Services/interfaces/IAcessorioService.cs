using ConcessionariaAPI.Models;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IAcessorioService
    {
        Task<Acessorio> Create(AcessorioDto entity);
        Task<List<Acessorio>> GetAll();
        Task<Acessorio> GetById(int id);
        Task<Acessorio> Update(int id, AcessorioDto entity);
        Task Delete(int id);
    }
}
