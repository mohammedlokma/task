using System.Linq.Expressions;

namespace task_api.Repositoy.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        Task CreateAsync(T entity);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null);
        Task RemoveAsync(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
