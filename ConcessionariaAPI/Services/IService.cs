namespace ConcessionariaAPI.Services
{
    public interface IService<T> where T : class
    {
        Task<T> Create(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Update(int id, T entity);
        Task Delete(int id);
    }
}
