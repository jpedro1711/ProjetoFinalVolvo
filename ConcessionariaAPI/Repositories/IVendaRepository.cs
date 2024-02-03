using ConcessionariaAPI.Models;

namespace ConcessionariaAPI.Repositories
{
    public interface IVendaRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task Delete(int id);
        Task<decimal> ValorVendasMesAno(int mes, int ano);
    }
}
