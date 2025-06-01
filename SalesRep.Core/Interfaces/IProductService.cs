using SalesRep.Core.Models;

namespace SalesRep.Core.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductListDto>> GetAllProductsAsync();
    }
}
