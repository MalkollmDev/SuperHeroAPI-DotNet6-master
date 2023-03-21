using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO.Homework;
using System.Text.RegularExpressions;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return BadRequest("User not found.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> Post(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("CheckUser")]
        public async Task<ActionResult<List<User>>> CheckUser(User model)
        {
            var user = await _context.Users
                    .Where(x => x.Login == model.Login && x.Password == model.Password)
                    .ToListAsync();

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<List<User>>> Update(User user)
        {
            var dbUser = await _context.Users.FindAsync(user.Id);
            if (dbUser == null)
                return NotFound("User not found.");

            _context.Users.Update(dbUser);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<User>>> Delete(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
                return NotFound("Hero not found.");

            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();

            return Ok(dbUser);
        }

    }
}
