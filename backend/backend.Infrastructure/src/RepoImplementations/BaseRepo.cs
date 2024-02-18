using Microsoft.EntityFrameworkCore;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Domain.src.Shared;
using backend.Infrastructure.src.Database;

namespace backend.Infrastructure.src.RepoImplementations
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly DatabaseContext _context;
        public BaseRepo(DatabaseContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
            _context = dbContext;
        }
        public virtual async Task<T> CreateOne(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteOneById(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAll(QueryOptions queryOptions)
        {
            var query = _dbSet.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(queryOptions.Search))
            {
                if(typeof(T) == typeof(User))
                {
                    query = query.Where(entity =>
                    ((User)(object)entity).FirstName.Contains(queryOptions.Search) ||
                    ((User)(object)entity).LastName.Contains(queryOptions.Search));
                }
                else if (typeof(T) == typeof(Category))
                {
                    query = query.Where(entity =>
                    ((Category)(object)entity).Name.Contains(queryOptions.Search));
                }
            }

            // Apply sorting
            
             if (!string.IsNullOrEmpty(queryOptions.Order))
            {
                var property = typeof(Product).GetProperty(queryOptions.Order);
                if (property != null)
                {
                    query = queryOptions.OrderByDescending ?
                        query.OrderByDescending(product => property.GetValue(product)) :
                        query.OrderBy(product => property.GetValue(product));
                }
            }
            
            // Apply pagination
            query = query
            .Skip(queryOptions.Offset)
            .Take(queryOptions.Limit);
            
            return await query.ToListAsync();
        }
        
        public async Task<T?> GetOneById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateOneById( T updatedEntity)
        {
            _dbSet.Update(updatedEntity);
            await _context.SaveChangesAsync();
            return updatedEntity;
        }
    }
}