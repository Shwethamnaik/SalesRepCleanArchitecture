using Microsoft.AspNetCore.Mvc;
using SalesRep.Core.Interfaces;
using SalesRep.Core.Models;

namespace SalesRep.API.Controllers
{
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
            //separate DTO for creation, which prevents identity column creation issue
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // 400 with validation errors
            await _service.Add(rep);

            return CreatedAtAction(nameof(GetById), rep); // Use the ID from the input object.
        }

        //[HttpPut]
        //[Route("Update")]
        //public async Task<IActionResult> Update([FromBody] SalesRepresentative rep)
        //{
        //    //is it good to pass id too in parameter?
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState); // 400 with validation errors
        //    var existing = await _service.GetById(rep.Id);
        //    if (existing == null)
        //        return NotFound();
        //    await _service.Update(rep);
        //    return NoContent();
        //}

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
            return NoContent(); // 204 — deletion successful
        }
    }
}
