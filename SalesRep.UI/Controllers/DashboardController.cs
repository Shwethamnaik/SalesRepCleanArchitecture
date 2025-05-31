using Microsoft.AspNetCore.Mvc;
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
            var salesData = _context.SalesRepresentatives
                .GroupBy(s => s.Name)
                .Select(g => new
                {
                    RepName = g.Key,
                    TotalSales = g.Sum(s => s.SalesPerformance)
                })
                .ToList();

            ViewBag.SalesLabels = salesData.Select(d => d.RepName).ToArray();
            ViewBag.SalesValues = salesData.Select(d => d.TotalSales).ToArray();


            return View("Dashboard");
        }
    }
}
