using Microsoft.EntityFrameworkCore;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Infrastructure.src.Database;

namespace backend.Infrastructure.src.RepoImplementations
{
    public class ShipmentRepo : BaseRepo<Shipment>, IShipmentRepo
    {
        private readonly DbSet<Shipment> _shipment;
        private readonly DatabaseContext _context;
        public ShipmentRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _shipment = dbContext.Shipments;
            _context = dbContext;
        }

        public async Task<Shipment> GetOneByOrderProductId(Guid orderProductId)
        {
            return await _shipment.FirstOrDefaultAsync(s => s.OrderProductId == orderProductId);
        }
    }
}