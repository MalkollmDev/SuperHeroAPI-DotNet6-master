using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO.Homework;
using SuperHeroAPI.Models.DTO.LessonDTO;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly DataContext _context;

        public HomeworksController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Homework>>> Get()
        {
            return Ok(await _context.Homeworks.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Homework>> Get(int id)
        {
            var homework = await _context.Homeworks.FindAsync(id);
            if (homework == null)
                return BadRequest("Homework not found.");
            return Ok(homework);
        }

        [HttpGet("GetHomeworkByGroupLesson/{groupId:int}/{lessonId:int}")]
        public async Task<ActionResult<List<Homework>>> GetHomeworkByGroupLesson(int groupId, int lessonId)
        {
            var model = await _context.Homeworks
                .Include(x => x.Lessons)
                .Include(x => x.Groups)
                .Where(x => x.GroupId == groupId && x.LessonId == lessonId)
                .ToListAsync();

            var result = new List<HomeworkDto>();
            foreach (var item in model)
            {
                result.Add(new HomeworkDto
                {
                    Id = item.Id,
                    LessonName = item.Lessons.Name,
                    NumberGroup = item.Groups.Number,
                    Task = item.Text,
                    Date = item.Date
                });
            }

            return Ok(result);
        }

        //[HttpGet("GetHomeworks")]
        //public async Task<ActionResult<List<Lesson>>> GetHomeworks()
        //{
        //    var model = await _context.Homeworks
        //        .Include(x => x.Lessons)
        //        .Include(x => x.Groups)
        //        .ToListAsync();

        //    var result = new List<LessonDto>();
        //    var _list = new List<int>();

        //    foreach (var item in model)
        //    {
        //        _list.Add(item.Groups.Number);
        //    }

        //    var uniqueList = _list.Distinct().ToList();

        //    foreach (var item in uniqueList)
        //    {
        //        var dto = new LessonDto
        //        {
        //            NumberGroup = item,
        //            LessonItems = GetLessonItems(item, model)
        //        };
        //        result.Add(dto);
        //    }

        //    return Ok(result);
        //}

        [HttpPost]
        public async Task<ActionResult<List<Homework>>> Post([FromForm] string text, [FromForm] DateTime date, [FromForm] int lessonId, [FromForm] int groupId)
        {
            var model = new Homework
            {
                Text = text,
                Date = date,
                LessonId = lessonId,
                GroupId = groupId
            };

            _context.Homeworks.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult<List<Homework>>> Update(Homework homework)
        {
            var dbHomework = await _context.Homeworks.FindAsync(homework.Id);
            if (dbHomework == null)
                return NotFound("Homework not found.");

            _context.Homeworks.Update(dbHomework);
            await _context.SaveChangesAsync();

            return Ok(homework);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Homework>>> Delete(int id)
        {
            var dbHomework = await _context.Homeworks.FindAsync(id);
            if (dbHomework == null)
                return NotFound("Hero not found.");

            _context.Homeworks.Remove(dbHomework);
            await _context.SaveChangesAsync();

            return Ok(dbHomework);
        }

    }
}
