namespace SalesRep.Core.Models
{
    public class SaleResponseDto
    {
        public int Id { get; set; }
        public int SalesRepId { get; set; }
        public string SalesRepName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
