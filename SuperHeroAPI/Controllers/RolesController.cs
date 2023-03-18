using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly DataContext _context;

        public RolesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Role>>> Get()
        {
            return Ok(await _context.Roles.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> Get(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
                return BadRequest("Role not found.");
            return Ok(role);
        }

        [HttpPost]
        public async Task<ActionResult<List<Role>>> Post(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return Ok(role);
        }

        [HttpPut]
        public async Task<ActionResult<List<Role>>> Update(Role role)
        {
            var dbRole = await _context.Roles.FindAsync(role.Id);
            if (dbRole == null)
                return NotFound("Role not found.");

            _context.Roles.Update(dbRole);
            await _context.SaveChangesAsync();            

            return Ok(role);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Role>>> Delete(int id)
        {
            var dbRole = await _context.Roles.FindAsync(id);
            if (dbRole == null)
                return NotFound("Hero not found.");

            _context.Roles.Remove(dbRole);
            await _context.SaveChangesAsync();

            return Ok(dbRole);
        }

    }
}
