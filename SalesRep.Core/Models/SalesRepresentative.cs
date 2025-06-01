namespace SalesRep.Core.Models
{
    public class SalesRepresentative
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
