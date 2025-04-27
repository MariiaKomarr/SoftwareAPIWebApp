using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareAPIWebApp.Models;

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
    }
}
