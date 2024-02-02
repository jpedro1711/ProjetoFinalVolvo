using ConcessionariaAPI.Models;

namespace ConcessionariaAPI.Repositories
{
    public interface IVeiculoRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        void Delete(int id);
        Task<List<Veiculo>> GetVeiculosByKilomers(int km, string system);
    }
}
