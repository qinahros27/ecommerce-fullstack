using Microsoft.EntityFrameworkCore;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Infrastructure.src.Database;

namespace backend.Infrastructure.src.RepoImplementations
{
    public class OrderProductRepo : BaseRepo<OrderProduct>, IOrderProductRepo
    {
        public OrderProductRepo(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}