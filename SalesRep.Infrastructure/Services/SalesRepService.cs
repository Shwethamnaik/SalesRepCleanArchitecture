using Microsoft.EntityFrameworkCore;
using SalesRep.Core.Interfaces;
using SalesRep.Core.Models;
using SalesRep.Infrastructure.Data;

namespace SalesRep.Infrastructure.Services
{
    public class SalesRepService : ISalesRepService
    {
        private readonly AppDbContext _context;
        public SalesRepService(AppDbContext context) => _context = context;

        public async Task<List<SalesRepresentative>> List()
        {
            return await _context.SalesRepresentatives.ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.SalesRepresentatives.AnyAsync(r => r.Id == id);
        }

        public async Task<SalesRepresentative?> GetById(int id)
        {
            return await _context.SalesRepresentatives.FindAsync(id);
        }
        public async Task Add(SalesRepresentative rep)
        {
            _context.Add(rep);
            await _context.SaveChangesAsync();
        }
        public async Task Update(SalesRepresentative rep)
        {
            _context.Update(rep); await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var rep = await _context.SalesRepresentatives.FindAsync(id);
            if (rep != null) { _context.Remove(rep); await _context.SaveChangesAsync(); }
        }
    }
}
