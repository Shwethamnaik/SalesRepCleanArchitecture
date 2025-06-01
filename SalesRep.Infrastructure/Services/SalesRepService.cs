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

        public async Task<List<SalesRepDetailDto>> List()
        {
            var reps = await _context.SalesRepresentatives
                .Select(r => new SalesRepDetailDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Region = r.Region
                })
                .ToListAsync();
            return reps;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.SalesRepresentatives.AnyAsync(r => r.Id == id);
        }

        public async Task<SalesRepDetailDto?> GetById(int id)
        {
            var salesRep = await _context.SalesRepresentatives
               .Where(r => r.Id == id)
               .Select(r => new SalesRepDetailDto
               {
                   Id = r.Id,
                   Name = r.Name,
                   Region = r.Region
               })
               .FirstOrDefaultAsync();
            return salesRep;
        }
        public async Task Add(SalesRepCreateDto dto)
        {
            var salesRep = new SalesRepresentative
            {
                Name = dto.Name,
                Region = dto.Region
            };

            _context.SalesRepresentatives.Add(salesRep);
            await _context.SaveChangesAsync();

            //_context.Add(rep);
            //await _context.SaveChangesAsync();
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
