using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace SalesRep.Core.Models
{
    public class SaleViewModel
    {
        public int Id { get; set; }

        [Required]
        public int SalesRepId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }

        public string? SalesRepName { get; set; }
        public string? ProductName { get; set; }

        // For dropdowns
        public List<SelectListItem>? SalesReps { get; set; }
        public List<SelectListItem>? Products { get; set; }
    }
}
