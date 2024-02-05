using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Models;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IEnderecoService
    {
        Task<Endereco> Create(EnderecoDto entity);
        Task<List<Endereco>> GetAll();
        Task<Endereco> GetById(int id);
        Task<Endereco> Update(int id, EnderecoDto entity);
        Task Delete(int id);
    }
}
