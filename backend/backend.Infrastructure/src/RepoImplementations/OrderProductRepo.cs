using Microsoft.EntityFrameworkCore;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Infrastructure.src.Database;

namespace backend.Infrastructure.src.RepoImplementations
{
    public class OrderProductRepo : BaseRepo<OrderProduct>, IOrderProductRepo
    {
        private readonly DbSet<OrderProduct> _orderProducts;
        private readonly DatabaseContext _context;
        public OrderProductRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _orderProducts = dbContext.OrderProducts;
            _context = dbContext;
        }

        public async Task<IEnumerable<OrderProduct>> GetAllByOrderId(Guid orderId)
        {
            return await _orderProducts.Where(rr => rr.OrderId == orderId).ToListAsync();
        }
    }
}