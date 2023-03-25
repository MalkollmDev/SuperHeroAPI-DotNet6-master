using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonGroupsController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonGroupsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<LessonGroup>>> Get()
        {
            return Ok(await _context.LessonGroups.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonGroup>> Get(int id)
        {
            var lessonGroup = await _context.LessonGroups.FindAsync(id);
            if (lessonGroup == null)
                return BadRequest("LessonGroup not found.");
            return Ok(lessonGroup);
        }

        [HttpPost]
        public async Task<ActionResult<List<LessonGroup>>> Post(LessonGroup lessonGroup)
        {
            _context.LessonGroups.Add(lessonGroup);
            await _context.SaveChangesAsync();

            return Ok(lessonGroup);
        }

        [HttpPut]
        public async Task<ActionResult<List<LessonGroup>>> Update(LessonGroup lessonGroup)
        {
            var dbLessonGroup = await _context.LessonGroups.FindAsync(lessonGroup.Id);
            if (dbLessonGroup == null)
                return NotFound("LessonGroup not found.");

            _context.LessonGroups.Update(dbLessonGroup);
            await _context.SaveChangesAsync();            

            return Ok(lessonGroup);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<LessonGroup>>> Delete(int id)
        {
            var dbLessonGroup = await _context.LessonGroups.FindAsync(id);
            if (dbLessonGroup == null)
                return NotFound("Hero not found.");

            _context.LessonGroups.Remove(dbLessonGroup);
            await _context.SaveChangesAsync();

            return Ok(dbLessonGroup);
        }

    }
}
