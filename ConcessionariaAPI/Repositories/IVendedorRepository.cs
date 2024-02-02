using ConcessionariaAPI.Repositories.Dto;

namespace ConcessionariaAPI.Repositories
{
    public interface IVendedorRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        void Delete(int id);
        Task<List<Salario>> GetSalarioMesAno(int id, int mes, int ano);
    }
}
