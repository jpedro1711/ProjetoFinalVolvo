using ConcessionariaAPI.Models;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IVeiculoService
    {
        Task<Veiculo> Create(VeiculoDto entity);
        Task<List<Veiculo>> GetAll();
        Task<Veiculo> GetById(int id);
        Task<Veiculo> Update(int id, VeiculoDto entity);
        Task Delete(int id);
        Task<List<Veiculo>> GetVeiculosByKilomers(int km, string system);
    }
}
