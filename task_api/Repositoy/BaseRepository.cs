using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using task_api.Data;
using task_api.Repositoy.Interfaces;
using task_api.Const;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace task_api.Repositoy
{
    public class BaseRepository<T>: IBaseRepository<T> where T: class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();

        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = dbSet;

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.AsNoTracking().SingleOrDefaultAsync(criteria);
        }
        public async Task<IEnumerable<T>> GetAllAsync(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
        {
            IQueryable<T> query = dbSet.Where(criteria);

            return await query.ToListAsync();
        }
       
        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);

        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return entities;
        }
        public void Attach(T entity)
        {
            dbSet.Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            dbSet.AttachRange(entities);
        }
    }
}
