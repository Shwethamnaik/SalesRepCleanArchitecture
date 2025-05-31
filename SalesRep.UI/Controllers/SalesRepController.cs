using Microsoft.AspNetCore.Mvc;
using SalesRep.Core.Interfaces;
using SalesRep.Core.Models;

namespace SalesRep.UI.Controllers
{
    public class SalesRepController : Controller
    {
        private readonly ISalesRepService _service;
        public SalesRepController(ISalesRepService service)
        {
            _service = service;
        }

        //public async Task<IActionResult> Index(string region, string product, string search)
        //{
        //    var reps = await _service.List(region, product, search);
        //    return View(reps);
        //}

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var salesReps = await _service.List();
            return View(salesReps);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var rep = await _service.GetById(id);
            if (rep == null)
                return NotFound();
            return View(rep);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSalesRepViewModel salesRepViewModel)
        {
            //create viewmodel instead of using salesRep directly
            var salesRep = new SalesRepresentative
            {
                Name = salesRepViewModel.Name,
                Product = salesRepViewModel.Product,
                Region = salesRepViewModel.Region,
                SalesPerformance = salesRepViewModel.SalesPerformance
            };
            if (ModelState.IsValid)
            {
                await _service.Add(salesRep);
                return RedirectToAction("List");
            }
            return View(salesRep);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var rep = await _service.GetById(id);
            if (rep == null)
                return NotFound();
            return View(rep);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SalesRepresentative salesRepModel)
        {
            var salesRep = await _service.GetById(salesRepModel.Id);
            if (salesRep is not null)
            {
                salesRep.Name = salesRepModel.Name;
                salesRep.Product = salesRepModel.Product;
                salesRep.Region = salesRepModel.Region;
                salesRep.SalesPerformance = salesRepModel.SalesPerformance;
                await _service.Update(salesRep);
            }
            return RedirectToAction("List", "SalesRep");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var rep = await _service.GetById(id);
            if (rep == null)
                return NotFound();
            else
            {
                await _service.Delete(id);
            }
            return RedirectToAction("List", "SalesRep");
        }
    }
}
