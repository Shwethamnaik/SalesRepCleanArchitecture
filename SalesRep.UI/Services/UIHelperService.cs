using Microsoft.AspNetCore.Mvc.Rendering;
using SalesRep.Core.Interfaces;
using SalesRep.UI.Services.Interfaces;

namespace SalesRep.UI.Services
{
    public class UIHelperService : IUIHelperService
    {
        private readonly ISalesRepService _salesRepService;
        private readonly IProductService _productService;

        public UIHelperService(ISalesRepService salesRepService, IProductService productService)
        {
            _salesRepService = salesRepService;
            _productService = productService;
        }

        public async Task<List<SelectListItem>> GetSalesRepSelectListAsync()
        {
            var salesReps = await _salesRepService.List();
            return salesReps.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetProductSelectListAsync()
        {
            var salesReps = await _productService.GetAllProductsAsync();
            return salesReps.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();
        }
    }
}
