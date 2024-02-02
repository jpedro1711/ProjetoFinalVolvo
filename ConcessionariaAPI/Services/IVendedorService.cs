using ConcessionariaAPI.Repositories.Dto;

namespace ConcessionariaAPI.Services
{
    public interface IVendedorService<T> where T : class
    {
        Task<T> Create(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Update(int id, T entity);
        void Delete(int id);
        Task<List<Salario>> GetSalarioMesAno(int id, int mes, int ano);
    }
}
