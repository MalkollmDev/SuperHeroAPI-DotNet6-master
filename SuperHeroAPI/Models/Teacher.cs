using System.Numerics;

namespace SuperHeroAPI.Models
{
    public class Teacher
    {
        public int Id { get; set; }        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
        public Teacher()
        {
            Lessons = new List<Lesson>();
        }
    }
}
