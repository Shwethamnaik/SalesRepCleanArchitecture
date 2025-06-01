using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesRep.UI.Services.Interfaces
{
    public interface IUIHelperService
    {
        Task<List<SelectListItem>> GetSalesRepSelectListAsync();
        Task<List<SelectListItem>> GetProductSelectListAsync();
    }
}
