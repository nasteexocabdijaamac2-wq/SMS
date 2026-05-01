using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sms.Data;
using Sms.Models;

namespace Sms.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Students>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // GET: api/students/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Students>> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id {id} not found.");
            }

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudent([FromBody] Students student)
        {
            if (student == null)
            {
                return BadRequest("Student data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
        }

        // PUT: api/students/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] Students studentUpdate)
        {
            if (studentUpdate == null)
            {
                return BadRequest("Updated student data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id {id} not found.");
            }

            student.Name = studentUpdate.Name;
            student.Phone = studentUpdate.Phone;
            student.Address = studentUpdate.Address;

            await _context.SaveChangesAsync();

            return Ok(student);
        }

        // DELETE: api/students/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound($"Student with Id {id} not found.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Student deleted successfully",
                data = student
            });
        }
    }
}