using Microsoft.EntityFrameworkCore;
using Npgsql;
using backend.Domain.src.Entities;

namespace backend.Infrastructure.src.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ReviewRate> ReviewRates { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<UserCard> UserCards { get; set; }

        public DatabaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("Default"));
            builder.MapEnum<Role>();
            builder.MapEnum<PaymentMethod>();
            builder.MapEnum<ShipmentState>();
            optionsBuilder.AddInterceptors(new TimeStampInterceptor());
            optionsBuilder.UseNpgsql(builder.Build()).UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Order>()
                        .HasOne(o => o.Payment)      // Order has one Payment
                        .WithOne(p => p.Order)       // Payment has one Order
                        .HasForeignKey<Payment>(p => p.OrderId); // Foreign key

            modelBuilder.Entity<Order>()
                        .HasOne(o => o.Shipment)   
                        .WithOne(s => s.Order)
                        .HasForeignKey<Shipment>(s => s.OrderId);     
            
            modelBuilder.Entity<User>()
                        .HasOne(u => u.UserCard)   
                        .WithOne(uc => uc.User)
                        .HasForeignKey<UserCard>(uc => uc.UserId);  

            modelBuilder.Entity<Order>()
                        .HasMany(o => o.OrderProducts)   
                        .WithOne(s => s.Order)
                        .HasForeignKey(s => s.OrderId);

            modelBuilder.Entity<Product>()
                        .HasMany(o => o.OrderProducts)   
                        .WithOne(s => s.Product)
                        .HasForeignKey(s => s.ProductId);
             
            modelBuilder.Entity<User>()
                        .HasMany(o => o.ReviewRates)   
                        .WithOne(s => s.User)
                        .HasForeignKey(s => s.UserId); 
            
            modelBuilder.Entity<Product>()
                        .HasMany(o => o.ReviewRates)   
                        .WithOne(s => s.Product)
                        .HasForeignKey(s => s.ProductId); 
            

            modelBuilder.HasPostgresEnum<Role>();

            modelBuilder.HasPostgresEnum<PaymentMethod>();

            modelBuilder.HasPostgresEnum<ShipmentState>();
    
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique(); 
        }
    }
}