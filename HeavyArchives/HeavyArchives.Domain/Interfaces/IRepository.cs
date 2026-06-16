namespace HeavyArchives.Domain.Interfaces
{
    public interface IRepository<T> where T:class
    {
        Task<T?> GetByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity); 
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(int id);
    }
}
