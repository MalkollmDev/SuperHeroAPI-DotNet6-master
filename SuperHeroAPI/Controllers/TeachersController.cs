using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using System.Numerics;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly DataContext _context;

        public TeachersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Teacher>>> Get()
        {
            return Ok(await _context.Teachers.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> Get(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return BadRequest("Teacher not found.");
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<List<Teacher>>> Post(Teacher teacher)
        {
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return Ok(teacher);
        }

        [HttpPut]
        public async Task<ActionResult<List<Teacher>>> Update(Teacher teacher)
        {
            var dbTeacher = await _context.Teachers.FindAsync(teacher.Id);
            if (dbTeacher == null)
                return NotFound("Teacher not found.");

            _context.Teachers.Update(dbTeacher);
            await _context.SaveChangesAsync();            

            return Ok(teacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Teacher>>> Delete(int id)
        {
            var dbTeacher = await _context.Teachers.FindAsync(id);
            if (dbTeacher == null)
                return NotFound("Hero not found.");

            _context.Teachers.Remove(dbTeacher);
            await _context.SaveChangesAsync();

            return Ok(dbTeacher);
        }

    }
}
