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
        public DbSet<LessonGroup> LessonGroups { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Appeal> Appeals { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ScheduleDate> ScheduleDates { get; set; }
    }
}
