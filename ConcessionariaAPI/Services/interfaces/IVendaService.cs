using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Models;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IVendaService
    {
        Task<Venda> Create(VendaDto entity);
        Task<List<Venda>> GetAll();
        Task<Venda> GetById(int id);
        Task<Venda> Update(int id, VendaDto entity);
        Task Delete(int id);
    }
}
