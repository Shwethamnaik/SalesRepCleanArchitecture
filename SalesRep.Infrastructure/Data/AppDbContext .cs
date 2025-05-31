using Microsoft.EntityFrameworkCore;
using SalesRep.Core.Models;

namespace SalesRep.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<SalesRepresentative> SalesRepresentatives { get; set; }
    }
}
