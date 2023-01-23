using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using System.Numerics;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var item = await _context.Events.FindAsync(id);
            if (item == null)
                return BadRequest("Event not found.");
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<List<Event>>> Post([FromForm] string title, [FromForm] string text, [FromForm] bool isPublished, [FromForm] IFormFileCollection files)
        {
            //_context.Events.Add(item);
            //await _context.SaveChangesAsync();

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
