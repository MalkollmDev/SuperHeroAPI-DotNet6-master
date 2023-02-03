using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO;

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
            return Ok(await _context.Events.Take(count).ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var item = await _context.Events.FindAsync(id);
            if (item == null)
                return BadRequest("Event not found.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<List<Event>>> Post([FromForm] string title, [FromForm] string content, [FromForm] bool isPublished, [FromForm] IFormFileCollection files)
        {
            var model = new Event
            {
                Title = title,
                Content = content,
                isPublished = isPublished,
                published = DateTime.Now
            };

            _context.Events.Add(model);
            await _context.SaveChangesAsync();

            foreach (var item in files)
            {
                var fileNameParts = item.FileName.Split('.');
                var stack = new Stack<string>(fileNameParts);

                using (var stream = new MemoryStream()) {
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
