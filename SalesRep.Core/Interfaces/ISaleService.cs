using SalesRep.Core.DTO;
using SalesRep.Core.Models;

namespace SalesRep.Core.Interfaces
{
    public interface ISaleService
    {
        Task<List<Sale>> GetSalesByRepIdAsync(int salesRepId);
        Task<List<SaleResponseDto>> GetSalesAsync();
        Task<(bool IsSuccess, string? Error, SaleResponseDto? Result)> AddSaleAsync(SaleCreateDto sale);
        Task<bool> ExistsAsync(int salesRepId, int productId, DateTime saleDate);
    }
}
