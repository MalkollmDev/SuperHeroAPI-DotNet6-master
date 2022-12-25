namespace SuperHeroAPI.Models
{
    public class Teacher
    {
        public int Id { get; set; }        
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Lesson { get; set; } = string.Empty;
    }
}
