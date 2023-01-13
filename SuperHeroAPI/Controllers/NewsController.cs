using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using System.Numerics;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly DataContext _context;

        public NewsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<New>>> Get()
        {
            return Ok(await _context.News.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<New>> Get(int id)
        {
            var new_item = await _context.News.FindAsync(id);
            if (new_item == null)
                return BadRequest("New not found.");
            return Ok(new_item);
        }

        [HttpPost]
        public async Task<ActionResult<List<New>>> Post(New item)
        {
            _context.News.Add(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        [HttpPut]
        public async Task<ActionResult<List<New>>> Update(New item)
        {
            var dbNew = await _context.News.FindAsync(item.Id);
            if (dbNew == null)
                return NotFound("New not found.");

            dbNew.Title = item.Title;
            dbNew.Preface = item.Preface;
            dbNew.Text = item.Text;
            dbNew.isPublished = item.isPublished;
            dbNew.published = item.published;

            await _context.SaveChangesAsync();

            return Ok(await _context.News.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<New>>> Delete(int id)
        {
            var dbNew = await _context.News.FindAsync(id);
            if (dbNew == null)
                return NotFound("New not found.");

            _context.News.Remove(dbNew);
            await _context.SaveChangesAsync();

            return Ok(dbNew);
        }

    }
}
