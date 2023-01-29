using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using System.Numerics;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonTimesController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonTimesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<LessonTime>>> Get()
        {
            return Ok(await _context.LessonTimes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonTime>> Get(int id)
        {
            var lessonTime = await _context.LessonTimes.FindAsync(id);
            if (lessonTime == null)
                return BadRequest("LessonTime not found.");
            return Ok(lessonTime);
        }

        [HttpPost]
        public async Task<ActionResult<List<LessonTime>>> Post(LessonTime lessonTime)
        {
            _context.LessonTimes.Add(lessonTime);
            await _context.SaveChangesAsync();

            return Ok(lessonTime);
        }

        [HttpPut]
        public async Task<ActionResult<List<LessonTime>>> Update(LessonTime lessonTime)
        {
            var dbLessonTime = await _context.LessonTimes.FindAsync(lessonTime.Id);
            if (dbLessonTime == null)
                return NotFound("LessonTime not found.");

            _context.LessonTimes.Update(dbLessonTime);
            await _context.SaveChangesAsync();            

            return Ok(lessonTime);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<LessonTime>>> Delete(int id)
        {
            var dbLessonTime = await _context.LessonTimes.FindAsync(id);
            if (dbLessonTime == null)
                return NotFound("LessonTime not found.");

            _context.LessonTimes.Remove(dbLessonTime);
            await _context.SaveChangesAsync();

            return Ok(dbLessonTime);
        }

    }
}
