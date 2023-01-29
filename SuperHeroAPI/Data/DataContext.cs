using SuperHeroAPI.Models;
using File = SuperHeroAPI.Models.File;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Lesson_Group> Lesson_Group { get; set; }
        public DbSet<File> Files { get; set; }
    }
}
