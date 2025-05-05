using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareAPIWebApp.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace SoftwareAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstallationsController : ControllerBase
    {
        private readonly SoftwareAPIContext _context;

        public InstallationsController(SoftwareAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Installation>>> GetInstallations()
        {
            return await _context.Installations
                .Include(i => i.Student)
                .Include(i => i.Software)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Installation>> GetInstallation(int id)
        {
            var installation = await _context.Installations
                .Include(i => i.Student)
                .Include(i => i.Software)
                .FirstOrDefaultAsync(i => i.InstallId == id);

            if (installation == null)
            {
                return NotFound();
            }

            return installation;
        }

        [HttpPost]
        public async Task<ActionResult<Installation>> PostInstallation(Installation installation)
        {
            _context.Installations.Add(installation);
            await _context.SaveChangesAsync();

            await _context.Entry(installation).Reference(i => i.Student).LoadAsync();
            await _context.Entry(installation).Reference(i => i.Software).LoadAsync();

            return CreatedAtAction(nameof(GetInstallation), new { id = installation.InstallId }, installation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstallation(int id, Installation installation)
        {
            if (id != installation.InstallId)
            {
                return BadRequest();
            }

            _context.Entry(installation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Installations.Any(e => e.InstallId == id))
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
        public async Task<IActionResult> DeleteInstallation(int id)
        {
            var installation = await _context.Installations.FindAsync(id);
            if (installation == null)
            {
                return NotFound();
            }

            _context.Installations.Remove(installation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchInstallation(int id, [FromBody] JsonPatchDocument<Installation> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var installation = _context.Installations.FirstOrDefault(i => i.InstallId == id);
            if (installation == null)
                return NotFound();

            patchDoc.ApplyTo(installation, error =>
            {
                ModelState.AddModelError(error.Operation?.path ?? "", error.ErrorMessage);

            });
            TryValidateModel(installation);

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
