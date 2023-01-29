﻿using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO.LessonDTO;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly DataContext _context;

        public LessonsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> Get()
        {
            var model = await _context.Lesson_Group
                .Include(x => x.Lessons)
                .Include(x => x.Groups)
                .Include(x => x.Teachers)
                .ToListAsync();

            var result = new List<LessonDto>();
            var _list = new List<int>();
            foreach (var item in model)
            {                
                _list.Add(item.Groups.Number);              
            }

            var uniqueList = _list.Distinct().ToList();

            foreach (var item in uniqueList)
            {
                var dto = new LessonDto
                {
                    NumberGroup = item,
                    LessonItems = GetLessonItems(item, model)
                };

                result.Add(dto);
            }

            return Ok(result);
        }

        private List<LessonItem> GetLessonItems(int number, List<Lesson_Group> model)
        {            
            var result = new List<LessonItem>();            

            foreach (var item in model)
            {
                if (number == item.Groups.Number)
                {
                    var dto = new LessonItem
                    {
                        LessonName = item.Lessons.Name,
                        LastName = item.Teachers.LastName,
                        FirstName = item.Teachers.FirstName,
                        MiddleName = item.Teachers.MiddleName,
                        Phone = item.Teachers.Phone,
                        Email = item.Teachers.Email,
                        IsReady = item.Teachers.IsReady
                    };
                    result.Add(dto);
                }

            }

            return result;
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
