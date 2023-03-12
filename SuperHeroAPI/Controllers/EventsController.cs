using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO;
using System.Collections.Generic;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> Get()
        {
            return Ok(await _context.Events.ToListAsync());
        }

        [HttpGet("GetPartEvents")]
        public async Task<ActionResult<List<Event>>> GetPartEvents(int count)
        {
            var models = await _context.Events
                .Include(X => X.Files)
                .Take(count)
                .ToListAsync();

            var events = new List<EventDTO>();
            foreach (var model in models)
            {
                var e = new EventDTO
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    IsPublished = model.isPublished,
                    Published = model.published
                };

                foreach (var file in model.Files)
                {
                    var fileDTO = new FileDTO
                    {
                        Id = file.Id,
                        Extension = file.Extension,
                        DownloadUrl = $"http://api.malkollm.ru/File/{file.Id}"
                    };

                    e.Files.Add(fileDTO);
                }

                events.Add(e);
            }
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var model = await _context.Events
                .Include(x => x.Files)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            var e = new EventDTO
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                IsPublished = model.isPublished,
                Published = model.published
            };

            foreach (var file in model.Files)
            {
                var fileDTO = new FileDTO
                {
                    Id = file.Id,
                    Extension = file.Extension,
                    DownloadUrl = $"http://api.malkollm.ru/File/{file.Id}"
                };

                e.Files.Add(fileDTO);
            }

            return Ok(e);
        }

        [HttpPost]
        public async Task<ActionResult<List<Event>>> Post(IFormCollection files)
        {
            bool myBool = Convert.ToBoolean(files["isPublished"]);
            var model = new Event
            {
                Title = files["title"],
                Content = files["content"],
                isPublished = myBool,
                published = DateTime.Now
            };

            _context.Events.Add(model);
            await _context.SaveChangesAsync();

            foreach (var item in files.Files)
            {
                var fileNameParts = item.FileName.Split('.');
                var stack = new Stack<string>(fileNameParts);

                using (var stream = new MemoryStream())
                {
                    item.CopyTo(stream);

                    var file = new Models.File
                    {
                        Extension = stack.Pop(),
                        OriginalName = string.Join(".", stack.ToArray().Reverse()),
                        EventId = model.Id,
                        Data = stream.GetBuffer()
                    };

                    _context.Files.Add(file);
                    await _context.SaveChangesAsync();
                }
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<List<Event>>> Update(Event item)
        {
            var dbNew = await _context.Events.FindAsync(item.Id);
            if (dbNew == null)
                return NotFound("New not found.");

            dbNew.Title = item.Title;
            dbNew.Content = item.Content;
            dbNew.isPublished = item.isPublished;
            dbNew.published = item.published;

            await _context.SaveChangesAsync();

            return Ok(await _context.Events.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Event>>> Delete(int id)
        {
            var dbNew = await _context.Events.FindAsync(id);
            if (dbNew == null)
                return NotFound("New not found.");

            _context.Events.Remove(dbNew);
            await _context.SaveChangesAsync();

            return Ok(dbNew);
        }

    }
}
