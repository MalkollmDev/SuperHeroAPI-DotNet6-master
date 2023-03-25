using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO.LessonDTO;
using SuperHeroAPI.Models.DTO.ScheduleDTO;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScheduleDatesController : ControllerBase
    {
        private readonly DataContext _context;

        public ScheduleDatesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ScheduleDate>>> Get()
        {
            var model = await _context.ScheduleDates
                .Include(x => x.LessonGroups)
                .ToListAsync();

            return Ok(model);
        }

        [HttpGet("GetScheduleByDate")]
        public async Task<ActionResult<List<ScheduleDate>>> GetScheduleByDate()
        {
            var model = await _context.ScheduleDates
                .Include(x => x.LessonGroups)
                .ThenInclude(x => x.Lessons)
                .Include(x => x.LessonGroups)
                .ThenInclude(x => x.Groups)
                .Include(x => x.LessonGroups)
                .ThenInclude(x => x.Teachers)
                .Include(x => x.LessonGroups)
                .ThenInclude(x => x.LessonTimes)
                .ToListAsync();

            var result = new List<ScheduleDto>();
            var _list = new List<DateTime>();

            foreach (var item in model)
            {
                _list.Add(item.Date.Date);
            }

            var uniqueList = _list.Distinct().ToList();

            foreach (var item in uniqueList)
            {
                var dto = new ScheduleDto
                {
                    Date = item.Date.ToString("dd/MM/yyyy"),
                    Lessons = GetLessons(item.Date, model)
                };

                result.Add(dto);
            }

            return Ok(result);
        }

        private List<LessonDto> GetLessons(DateTime date, List<ScheduleDate> model)
        {
            var result = new List<LessonDto>();
            var _listLessonGroup = new List<LessonGroup>();

            var date1 = date.ToString("dd/MM/yyyy");

            foreach (var item in model)
            {
                _listLessonGroup.Add(item.LessonGroups);

                if (item.Date.ToString("dd/MM/yyyy") == date1)
                {
                    var dto = new LessonDto
                    {
                        NumberGroup = item.LessonGroups.Groups.Number,
                        LessonItems = GetLessonItems(item.LessonGroups.Groups.Number, _listLessonGroup)
                    };

                    result.Add(dto);
                }
            }

            return result;
        }

        private List<LessonItemDto> GetLessonItems(int number, List<LessonGroup> model)
        {
            var result = new List<LessonItemDto>();

            foreach (var item in model)
            {
                if (number == item.Groups.Number)
                {
                    var dto = new LessonItemDto
                    {
                        LessonName = item.Lessons.Name,
                        LastName = item.Teachers.LastName,
                        FirstName = item.Teachers.FirstName,
                        MiddleName = item.Teachers.MiddleName,
                        Phone = item.Teachers.Phone,
                        Email = item.Teachers.Email,
                        IsReady = item.Teachers.IsReady,
                        LessonStart = item.LessonTimes.LessonStart,
                        LessonEnd = item.LessonTimes.LessonEnd,
                        LessonBreak = item.LessonTimes.LessonBreak
                    };

                    result.Add(dto);
                }
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDate>> Get(int id)
        {
            var scheduleDate = await _context.ScheduleDates.FindAsync(id);
            if (scheduleDate == null)
                return BadRequest("ScheduleDate not found.");
            return Ok(scheduleDate);
        }

        [HttpPost]
        public async Task<ActionResult<List<ScheduleDate>>> Post(ScheduleDate scheduleDate)
        {
            _context.ScheduleDates.Add(scheduleDate);
            await _context.SaveChangesAsync();

            return Ok(scheduleDate);
        }

        [HttpPut]
        public async Task<ActionResult<List<ScheduleDate>>> Update(ScheduleDate scheduleDate)
        {
            var dbScheduleDate = await _context.ScheduleDates.FindAsync(scheduleDate.Id);
            if (dbScheduleDate == null)
                return NotFound("ScheduleDate not found.");

            _context.ScheduleDates.Update(dbScheduleDate);
            await _context.SaveChangesAsync();            

            return Ok(scheduleDate);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ScheduleDate>>> Delete(int id)
        {
            var dbScheduleDate = await _context.ScheduleDates.FindAsync(id);
            if (dbScheduleDate == null)
                return NotFound("Hero not found.");

            _context.ScheduleDates.Remove(dbScheduleDate);
            await _context.SaveChangesAsync();

            return Ok(dbScheduleDate);
        }

    }
}
