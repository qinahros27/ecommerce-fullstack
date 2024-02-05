using Microsoft.EntityFrameworkCore;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Domain.src.Shared;
using backend.Infrastructure.src.Database;

namespace backend.Infrastructure.src.RepoImplementations
{
    public class ProductRepo : IProductRepo
    {
        private readonly DbSet<Product> _dbSet;
        private readonly DatabaseContext _context;
        public ProductRepo(DatabaseContext dbContext) 
        {
            _dbSet = dbContext.Set<Product>();
            _context = dbContext;
        }

        public async Task<Product> CreateOne(Product entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteOneById(Product entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Product>> GetAll(QueryOptionProduct queryOptions)
        {
            var query = _dbSet.AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(queryOptions.Search))
            {
                query = query.Where(entity =>
                ((Product)(object)entity).Title.Contains(queryOptions.Search));
                Console.WriteLine(query);
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

            // Apply filtering by MinPrice
            if (queryOptions.MinPrice > 0)
            {
                query = query.Where(product => product.Price >= queryOptions.MinPrice);
            }

            // Apply filtering by MaxPrice
            if (queryOptions.MaxPrice > 0)
            {
                query = query.Where(product => product.Price <= queryOptions.MaxPrice);
            }

            // Apply filtering by CategoryId
            if (queryOptions.CategoryId != Guid.Empty)
            {
                query = query.Where(product => product.CategoryId == queryOptions.CategoryId);
            }

            // Apply pagination
            query = query
            .Skip(queryOptions.Offset)
            .Take(queryOptions.Limit);
            
            return await query.ToListAsync();
        }
        
        public async Task<Product?> GetOneById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Product> UpdateOneById(Product updatedEntity)
        {
            _dbSet.Update(updatedEntity);
            await _context.SaveChangesAsync();
            return updatedEntity;
        }
    }
}