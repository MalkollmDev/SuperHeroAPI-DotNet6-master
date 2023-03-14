namespace SuperHeroAPI.Models
{
    public class Homework
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int LessonId { get; set; }
        public int GroupId { get; set; }
        public Lesson Lessons { get; set; }
        public Group Groups { get; set; }
    }
}
