using Microsoft.AspNetCore.Mvc.Rendering;
using SalesRep.Core.DTO;

namespace SalesRep.UI.ViewModel
{
    public class SalesFilterViewModel
    {
        public List<SaleResponseDto> Sales { get; set; }

        public int? SalesRepId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public List<SelectListItem> SalesReps { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
