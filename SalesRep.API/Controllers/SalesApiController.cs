using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesRep.Core.DTO;
using SalesRep.Core.Interfaces;

namespace SalesRep.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesApiController : ControllerBase
    {
        private readonly ISaleService _saleRepo;

        public SalesApiController(ISaleService saleRepo)
        {
            _saleRepo = saleRepo;
        }

        [Route("GetAllSales")]
        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var sales = await _saleRepo.GetSalesAsync();
            return Ok(sales);
        }

        [Route("AddSale")]
        [HttpPost]
        public async Task<IActionResult> AddSale([FromBody] SaleCreateDto sale)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _saleRepo.AddSaleAsync(sale);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(new { result.Result!.Id });
        }
    }
}
