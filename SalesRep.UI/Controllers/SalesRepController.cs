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
            var salesRep = new SalesRepCreateDto
            {
                Name = salesRepViewModel.Name,
                Region = salesRepViewModel.Region,
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
            SalesRepresentative SalesRep = new SalesRepresentative
            {
                Id = salesRepModel.Id,
                Name = salesRepModel.Name,
                Region = salesRepModel.Region
            };
            await _service.Update(SalesRep);
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
