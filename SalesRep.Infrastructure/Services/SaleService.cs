using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesRep.Core.DTO;
using SalesRep.Core.Interfaces;
using SalesRep.Core.Models;
using SalesRep.Infrastructure.Data;

namespace SalesRep.Infrastructure.Services
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;
        public SaleService(AppDbContext context) => _context = context;
        public async Task<(bool IsSuccess, string? Error, SaleResponseDto? Result)> AddSaleAsync([FromBody] SaleCreateDto dto)
        {
            var product = await _context.Products.FindAsync(dto.ProductId);
            if (product == null)
                return (false, "Invalid ProductId", null);
            var salesRep = await _context.SalesRepresentatives.FindAsync(dto.SalesRepId);
            if (salesRep == null)
                return (false, "Invalid SalesRepId", null);
            if (dto.Amount <= 0)
                return (false, "Amount must be greater than zero", null);
            if (dto.SaleDate > DateTime.UtcNow)
                return (false, "Sale date cannot be in the future", null);
            var sale = new Sale
            {
                SalesRepId = dto.SalesRepId,
                ProductId = dto.ProductId,
                Amount = dto.Amount,
                SaleDate = dto.SaleDate
            };
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            var salesRepDetail = await _context.SalesRepresentatives.FindAsync(dto.SalesRepId);
            var productDetail = await _context.Products.FindAsync(dto.ProductId);

            var response = new SaleResponseDto
            {
                Id = sale.Id,
                SalesRepId = sale.SalesRepId,
                SalesRepName = salesRep?.Name ?? string.Empty,
                ProductId = sale.ProductId,
                ProductName = product?.Name ?? string.Empty,
                Amount = sale.Amount,
                SaleDate = sale.SaleDate
            };


            return (true, null, response);
        }

        public async Task<List<SaleResponseDto>> GetSalesAsync()
        {
            return await _context.Sales
                .Include(s => s.SalesRep)
                .Include(s => s.Product)
                .Select(s => new SaleResponseDto
                {
                    Id = s.Id,
                    SalesRepId = s.SalesRepId,
                    SalesRepName = s.SalesRep.Name,
                    ProductId = s.ProductId,
                    ProductName = s.Product.Name,
                    Amount = s.Amount,
                    SaleDate = s.SaleDate
                })
                .ToListAsync();
        }

        public async Task<List<Sale>> GetSalesByRepIdAsync(int salesRepId)
        {
            return await _context.Sales.Where(x => x.SalesRepId == salesRepId).ToListAsync();
        }

        public async Task<bool> ExistsAsync(int salesRepId, int productId, DateTime saleDate)
        {
            return await _context.Sales.AnyAsync(s =>
                s.SalesRepId == salesRepId &&
                s.ProductId == productId &&
                s.SaleDate.Date == saleDate.Date);
        }
    }
}
