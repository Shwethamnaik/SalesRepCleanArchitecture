using SalesRep.Core.Models;

namespace SalesRep.Core.Interfaces
{
    public interface ISalesRepService
    {
        Task<List<SalesRepresentative>> List();
        Task<bool> Exists(int id);
        Task<SalesRepresentative> GetById(int id);
        Task Add(SalesRepresentative rep);
        Task Update(SalesRepresentative rep);
        Task Delete(int id);
    }
}
