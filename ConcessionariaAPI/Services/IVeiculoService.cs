using ConcessionariaAPI.Models;

namespace ConcessionariaAPI.Services
{
    public interface IVeiculoService<T> where T : class
    {
        Task<T> Create(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Update(int id, T entity);
        void Delete(int id);
        Task<List<Veiculo>> GetVeiculosByKilomers(int km, string system);
    }
}
