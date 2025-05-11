using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareAPIWebApp.Models;
using Microsoft.AspNetCore.JsonPatch;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Softwares.Add(software);
            await _context.SaveChangesAsync();

            _context.Entry(software).Reference(s => s.Type).Load();

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

        [HttpPatch("{id}")]
        public IActionResult PatchSoftware(int id, [FromBody] JsonPatchDocument<Software> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var software = _context.Softwares.FirstOrDefault(s => s.SoftwareId == id);
            if (software == null)
                return NotFound();

            patchDoc.ApplyTo(software, error =>
            {
                ModelState.AddModelError(error.Operation?.path ?? "", error.ErrorMessage);

            });
            TryValidateModel(software);

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

        [HttpGet("html")]
        public IActionResult GetHtmlPage()
        {
            var html = @"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8'>
        <title>Software Library</title>
        <link rel='stylesheet' href='/css/site.css' />
    </head>
    <body>
        <h1>Software Library</h1>
        <p>Сторінка з прикладом стилів.</p>
    </body>
    </html>";

            return Content(html, "text/html");
        }

    }
}
