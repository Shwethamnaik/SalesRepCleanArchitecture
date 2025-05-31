namespace SalesRep.Core.Models
{
    public class SalesRepresentative
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Product { get; set; }
        public decimal SalesPerformance { get; set; }
    }
}
