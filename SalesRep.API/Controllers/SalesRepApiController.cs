using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesRep.Core.DTO;
using SalesRep.Core.Interfaces;

namespace SalesRep.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalesRepApiController : ControllerBase
    {

        private readonly ISalesRepService _service;
        public SalesRepApiController(ISalesRepService service) => _service = service;

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var reps = await _service.List();
            return Ok(reps);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var reps = await _service.GetById(id);
            if (reps != null)
                return Ok(reps);
            else
                return NotFound(new { message = $"Resource with ID {id} not found." });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(SalesRepCreateDto rep)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 with validation errors
            await _service.Add(rep);

            return CreatedAtAction(nameof(GetById), rep);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid ID." });
            var exists = await _service.Exists(id);
            if (!exists)
                return NotFound(new { message = $"SalesRepresentative with ID {id} not found." });

            await _service.Delete(id);
            return NoContent();
        }
    }
}
