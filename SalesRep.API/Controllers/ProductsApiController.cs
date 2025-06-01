using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesRep.Core.Interfaces;

namespace SalesRep.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService _productRepo;

        public ProductsApiController(IProductService productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var products = await _productRepo.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
