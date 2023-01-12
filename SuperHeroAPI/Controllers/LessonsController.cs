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
            //Teacher t1 = new() { LastName = "Фаткуллин", FirstName = "Марсель", MiddleName = "Саматович" };
            //Teacher t2 = new() { LastName = "Парадова", FirstName = "Юлия", MiddleName = "Хусаиновна" };
            //_context.Teachers.Add(t1);

            //_context.Teachers.Add(t2);
            //_context.SaveChanges();

            //Lesson pl1 = new() { Name = "Физика", TeacherId = t1.Id };
            //Lesson pl2 = new() { Name = "Психология", Teacher = t2 };
            //_context.Lessons.AddRange(new List<Lesson> { pl1, pl2 });
            //_context.SaveChanges();


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

            dbLesson.Name = lesson.Name;

            if (lesson.TeacherId != null)
            {
                dbLesson.TeacherId = lesson.TeacherId;
            }

            await _context.SaveChangesAsync();
            var result = _context.Lessons.Include(l => l.Teacher).Where(l => l.Id == dbLesson.Id);

            return Ok(result);
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
