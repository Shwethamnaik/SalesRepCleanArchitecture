using Microsoft.EntityFrameworkCore;
using SalesRep.Core.Models;

namespace SalesRep.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<SalesRepresentative> SalesRepresentatives { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.SalesRep)
                .WithMany(r => r.Sales)
                .HasForeignKey(s => s.SalesRepId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Product)
                .WithMany(p => p.Sales)
                .HasForeignKey(s => s.ProductId);
        }

    }
}
