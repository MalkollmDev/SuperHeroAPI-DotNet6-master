using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppealsController : ControllerBase
    {
        private readonly DataContext _context;

        public AppealsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Appeal>>> Get()
        {
            return Ok(await _context.Appeals.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appeal>> Get(int id)
        {
            var appeal = await _context.Appeals.FindAsync(id);
            if (appeal == null)
                return BadRequest("Appeal not found.");
            return Ok(appeal);
        }

        [HttpPost]
        public async Task<ActionResult<List<Appeal>>> Post(
            [FromForm] string lastName, 
            [FromForm] string firstName, 
            [FromForm] string middleName, 
            [FromForm] string phone, 
            [FromForm] string text,
            [FromForm] string email
            )
        {
            var model = new Appeal
            {
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                Phone = phone,
                Text = text,
                Date = DateTime.Now,
                Email = email
            };

            _context.Appeals.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult<List<Appeal>>> Update(Appeal appeal)
        {
            var dbAppeal = await _context.Appeals.FindAsync(appeal.Id);
            if (dbAppeal == null)
                return NotFound("Appeal not found.");

            _context.Appeals.Update(dbAppeal);
            await _context.SaveChangesAsync();            

            return Ok(appeal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Appeal>>> Delete(int id)
        {
            var dbAppeal = await _context.Appeals.FindAsync(id);
            if (dbAppeal == null)
                return NotFound("Appeal not found.");

            _context.Appeals.Remove(dbAppeal);
            await _context.SaveChangesAsync();

            return Ok(dbAppeal);
        }

    }
}
