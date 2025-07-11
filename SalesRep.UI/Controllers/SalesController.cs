﻿using Microsoft.AspNetCore.Mvc;
using SalesRep.Core.DTO;
using SalesRep.Core.Interfaces;
using SalesRep.UI.Services.Interfaces;
using SalesRep.UI.ViewModel;

namespace SalesRep.UI.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISaleService _service;
        private readonly IUIHelperService _uiHelper;
        public SalesController(ISaleService service, IUIHelperService uiHelper)
        {
            _service = service;
            _uiHelper = uiHelper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(SalesFilterViewModel filter)
        {
            filter.SalesReps = await _uiHelper.GetSalesRepSelectListAsync();
            filter.Products = await _uiHelper.GetProductSelectListAsync();
            filter.Sales = await _service.FilterSalesAsync(filter);
            filter.Regions = await _uiHelper.GetRegionSelectListAsync();
            filter.Sales = await _service.FilterSalesAsync(filter);
            return View(filter);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new SaleResponseDto
            {
                SalesReps = await _uiHelper.GetSalesRepSelectListAsync(),
                Products = await _uiHelper.GetProductSelectListAsync(),
                SaleDate = DateTime.Today
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleResponseDto model)
        {
            if (!ModelState.IsValid)
            {
                model.SalesReps = await _uiHelper.GetSalesRepSelectListAsync();
                model.Products = await _uiHelper.GetProductSelectListAsync();
                return View(model);
            }

            var dto = new SaleCreateDto
            {
                SalesRepId = model.SalesRepId,
                ProductId = model.ProductId,
                SaleDate = model.SaleDate,
                Amount = model.Amount
            };
            if (model.Amount <= 0)
            {
                ModelState.AddModelError(nameof(model.Amount), "Amount must be greater than 0.");
            }
            bool isDuplicate = await _service.ExistsAsync(model.SalesRepId, model.ProductId, model.SaleDate);
            if (isDuplicate)
            {
                ModelState.AddModelError("", "A sale already exists for this Sales Rep, Product, and Date.");
            }
            if (!ModelState.IsValid)
            {
                model.SalesReps = await _uiHelper.GetSalesRepSelectListAsync();
                model.Products = await _uiHelper.GetProductSelectListAsync();
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var response = await _service.AddSaleAsync(dto);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
