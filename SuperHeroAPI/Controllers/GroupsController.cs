using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using System.Numerics;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly DataContext _context;

        public GroupsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Group>>> Get()
        {
            return Ok(await _context.Groups.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> Get(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
                return BadRequest("Group not found.");
            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult<List<Group>>> Post(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return Ok(group);
        }

        [HttpPut]
        public async Task<ActionResult<List<Group>>> Update(Group group)
        {
            var dbGroup = await _context.Groups.FindAsync(group.Id);
            if (dbGroup == null)
                return NotFound("Group not found.");

            _context.Groups.Update(dbGroup);
            await _context.SaveChangesAsync();            

            return Ok(group);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Group>>> Delete(int id)
        {
            var dbGroup = await _context.Groups.FindAsync(id);
            if (dbGroup == null)
                return NotFound("Hero not found.");

            _context.Groups.Remove(dbGroup);
            await _context.SaveChangesAsync();

            return Ok(dbGroup);
        }

    }
}
