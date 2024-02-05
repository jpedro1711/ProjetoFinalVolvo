namespace ConcessionariaAPI.Models.dtos
{
    public interface IDto<T> where T : class
    {
        T ToEntity();
    }
}
