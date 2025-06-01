using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SalesRep.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Use your actual PostgreSQL connection string here
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SalesRep1;Username=postgres;Password=Abc123@@");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
