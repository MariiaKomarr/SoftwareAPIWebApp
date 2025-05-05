using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareAPIWebApp.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace SoftwareAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareTypesController : ControllerBase
    {
        private readonly SoftwareAPIContext _context;

        public SoftwareTypesController(SoftwareAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoftwareType>>> GetSoftwareTypes()
        {
            return await _context.SoftwareTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SoftwareType>> GetSoftwareType(int id)
        {
            var softwareType = await _context.SoftwareTypes.FindAsync(id);

            if (softwareType == null)
            {
                return NotFound();
            }

            return softwareType;
        }

        [HttpPost]
        public async Task<ActionResult<SoftwareType>> PostSoftwareType(SoftwareType softwareType)
        {
            _context.SoftwareTypes.Add(softwareType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSoftwareType), new { id = softwareType.TypeId }, softwareType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftwareType(int id, SoftwareType softwareType)
        {
            if (id != softwareType.TypeId)
            {
                return BadRequest();
            }

            _context.Entry(softwareType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.SoftwareTypes.Any(e => e.TypeId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoftwareType(int id)
        {
            var softwareType = await _context.SoftwareTypes.FindAsync(id);
            if (softwareType == null)
            {
                return NotFound();
            }

            _context.SoftwareTypes.Remove(softwareType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchSoftwareType(int id, [FromBody] JsonPatchDocument<SoftwareType> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var softwareType = _context.SoftwareTypes.FirstOrDefault(s => s.TypeId == id);
            if (softwareType == null)
                return NotFound();

            patchDoc.ApplyTo(softwareType, error =>
            {
                ModelState.AddModelError(error.Operation?.path ?? "", error.ErrorMessage);

            });
            TryValidateModel(softwareType);

            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _context.SaveChanges();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,PUT,DELETE,PATCH,OPTIONS");
            return Ok();
        }
    }
}
