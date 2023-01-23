using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using System.Numerics;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonsContoller : ControllerBase
    {
        private readonly DataContext _context;

        public LessonsContoller(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> Get()
        {
            return Ok(await _context.Lessons.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> Get(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
                return BadRequest("Hero not found.");
            return Ok(lesson);
        }

        [HttpPost]
        public async Task<ActionResult<List<Lesson>>> Post(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            await _context.SaveChangesAsync();

            return Ok(lesson);
        }

        [HttpPut]
        public async Task<ActionResult<List<Lesson>>> Update(Lesson lesson)
        {
            var dbLesson = await _context.Lessons.FindAsync(lesson.Id);
            if (dbLesson == null)
                return NotFound("Lesson not found.");

            _context.Lessons.Update(dbLesson);
            await _context.SaveChangesAsync();            

            return Ok(lesson);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Lesson>>> Delete(int id)
        {
            var dbLesson = await _context.Lessons.FindAsync(id);
            if (dbLesson == null)
                return NotFound("Hero not found.");

            _context.Lessons.Remove(dbLesson);
            await _context.SaveChangesAsync();

            return Ok(dbLesson);
        }

    }
}
