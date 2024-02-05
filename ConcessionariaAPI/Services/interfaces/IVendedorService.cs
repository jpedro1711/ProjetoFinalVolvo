using ConcessionariaAPI.Repositories.Dto;

namespace ConcessionariaAPI.Services.interfaces
{
    public interface IVendedorService<T> where T : class
    {
        Task<T> Create(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Update(int id, T entity);
        Task Delete(int id);
        Task<List<Salario>> GetSalarioMesAno(int id, int mes, int ano);
        Task<List<List<Salario>>> GetSalarioVendedores();
    }
}
