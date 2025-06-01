namespace SalesRep.Core.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int SalesRepId { get; set; }
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Amount { get; set; }

        public SalesRepresentative SalesRep { get; set; } = null!;
        public Product Product { get; set; } = null!;

    }
}
