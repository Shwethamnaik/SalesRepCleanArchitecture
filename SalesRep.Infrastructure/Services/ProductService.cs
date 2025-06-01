using Microsoft.EntityFrameworkCore;
using SalesRep.Core.Interfaces;
using SalesRep.Core.Models;
using SalesRep.Infrastructure.Data;

namespace SalesRep.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context) => _context = context;
        public async Task<List<ProductListDto>> GetAllProductsAsync()
        {
            return await _context.Products.Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToListAsync();
        }
    }
}
