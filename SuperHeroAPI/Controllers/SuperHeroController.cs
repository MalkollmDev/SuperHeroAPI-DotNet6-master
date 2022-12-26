using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using System.Numerics;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("ping")]
        public ActionResult Ping()
        {
            return Ok("Ping Pong!");
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        //[HttpPost]
        //public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        //{
        //    _context.SuperHeroes.Add(hero);
        //    await _context.SaveChangesAsync();

        //    return Ok(await _context.SuperHeroes.ToListAsync());
        //}

        [HttpPost]
        public async Task<ActionResult<List<Teacher>>> AddTeacher()
        {
            // создание и добавление моделей
            Teacher t1 = new() { LastName = "Фаткуллин", FirstName = "Марсель", MiddleName = "Саматович" };
            Teacher t2 = new() { LastName = "Парадова", FirstName = "Юлия", MiddleName = "Хусаиновна" };
            _context.Teachers.Add(t1);
            _context.Teachers.Add(t2);
            _context.SaveChanges();

            Lesson pl1 = new() { Name = "Физика", Teacher = t1 };
            Lesson pl2 = new() { Name = "Психология", Teacher = t2 };
            _context.Lessons.AddRange(new List<Lesson> { pl1, pl2 });
            _context.SaveChanges();


            //_context.Teachers.Add(teacher);
            //await _context.SaveChangesAsync();

            return Ok(await _context.Teachers.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero not found.");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

    }
}
