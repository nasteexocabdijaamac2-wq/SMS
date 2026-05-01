using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sms.Models;
using Sms.Data;

namespace Sms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClassesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classes>>> GetClasses()
        {
            var classes = await _context.Classes
                .Include(c => c.Students)
                .ToListAsync();

            return Ok(classes);
        }

        // GET: api/classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classes>> GetClass(int id)
        {
            var existingClass = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingClass == null)
            {
                return NotFound($"Class with Id {id} not found.");
            }

            return Ok(existingClass);
        }

        // POST: api/classes
        [HttpPost]
        public async Task<ActionResult<Classes>> Post([FromBody] Classes newClass)
        {
            if (newClass == null)
            {
                return BadRequest("Class cannot be null.");
            }

            _context.Classes.Add(newClass);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClass), new { id = newClass.Id }, newClass);
        }

        // PUT: api/classes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Classes updatedClass)
        {
            if (updatedClass == null)
            {
                return BadRequest("Updated class data is required.");
            }

            var existingClass = await _context.Classes.FindAsync(id);

            if (existingClass == null)
            {
                return NotFound($"Class with Id {id} not found.");
            }

            existingClass.Name = updatedClass.Name;
            existingClass.Shift = updatedClass.Shift;
            existingClass.Date = updatedClass.Date;

            await _context.SaveChangesAsync();

            return Ok(existingClass);
        }

        // DELETE: api/classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingClass = await _context.Classes.FindAsync(id);

            if (existingClass == null)
            {
                return NotFound($"Class with Id {id} not found.");
            }

            _context.Classes.Remove(existingClass);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Class deleted successfully", data = existingClass });
        }
    }
}