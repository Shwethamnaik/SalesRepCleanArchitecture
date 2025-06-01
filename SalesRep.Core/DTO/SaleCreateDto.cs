using System.ComponentModel.DataAnnotations;

namespace SalesRep.Core.DTO
{
    public class SaleCreateDto
    {
        [Required]
        public int SalesRepId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; }
    }
}
