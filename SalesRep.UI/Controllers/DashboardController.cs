using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesRep.Infrastructure.Data;

namespace SalesRep.UI.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var salesData = _context.Sales
                .Include(s => s.SalesRep)
                .GroupBy(s => s.SalesRep.Name)
                .Select(g => new
                {
                    RepName = g.Key,
                    TotalSales = g.Sum(s => s.Amount)
                })
                .ToList();
            ViewBag.SalesLabels = salesData.Select(d => d.RepName).ToArray();
            ViewBag.SalesValues = salesData.Select(d => d.TotalSales).ToArray();


            return View("Dashboard");
        }
    }
}
