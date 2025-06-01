using SalesRep.Core.DTO;
using SalesRep.Core.Models;

namespace SalesRep.Core.Interfaces
{
    public interface ISalesRepService
    {
        Task<List<SalesRepDetailDto>> List();
        Task<bool> Exists(int id);
        Task<SalesRepDetailDto> GetById(int id);
        Task Add(SalesRepCreateDto rep);
        Task Update(SalesRepresentative rep);
        Task Delete(int id);
    }
}
