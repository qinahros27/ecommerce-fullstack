using Microsoft.EntityFrameworkCore;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Infrastructure.src.Database;

namespace backend.Infrastructure.src.RepoImplementations
{
    public class OrderRepo : BaseRepo<Order>, IOrderRepo
    {
        private readonly DbSet<Order> _orders;
        private readonly DatabaseContext _context;
        public OrderRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _orders = dbContext.Orders;
            _context = dbContext;
        }

        public async Task<IEnumerable<Order>> GetAllByUserId(Guid userId)
        {
            return await _orders.Where(o => o.UserId == userId).ToListAsync();
        }
    }
}