using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareAPIWebApp.Models;

namespace SoftwareAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwaresController : ControllerBase
    {
        private readonly SoftwareAPIContext _context;

        public SoftwaresController(SoftwareAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Software>>> GetSoftwares()
        {
            return await _context.Softwares.Include(s => s.Type).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Software>> GetSoftware(int id)
        {
            var software = await _context.Softwares.Include(s => s.Type).FirstOrDefaultAsync(s => s.SoftwareId == id);

            if (software == null)
            {
                return NotFound();
            }

            return software;
        }

        [HttpPost]
        public async Task<ActionResult<Software>> PostSoftware(Software software)
        {
            _context.Softwares.Add(software);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSoftware), new { id = software.SoftwareId }, software);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftware(int id, Software software)
        {
            if (id != software.SoftwareId)
            {
                return BadRequest();
            }

            _context.Entry(software).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Softwares.Any(e => e.SoftwareId == id))
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
        public async Task<IActionResult> DeleteSoftware(int id)
        {
            var software = await _context.Softwares.FindAsync(id);
            if (software == null)
            {
                return NotFound();
            }

            _context.Softwares.Remove(software);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
