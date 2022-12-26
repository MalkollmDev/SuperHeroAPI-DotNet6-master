namespace SuperHeroAPI.Models
{
    public class Lesson
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
