using ConcessionariaAPI.Models;
using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Repositories.Dto;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IVendedorService
    {
        Task<Vendedor> Create(VendedorDto entity);
        Task<List<Vendedor>> GetAll();
        Task<Vendedor> GetById(int id);
        Task<Vendedor> Update(int id, VendedorDto entity);
        Task Delete(int id);
        Task<List<Salario>> GetSalarioMesAno(int id, int mes, int ano);
        Task<List<List<Salario>>> GetSalarioVendedores();
    }
}
