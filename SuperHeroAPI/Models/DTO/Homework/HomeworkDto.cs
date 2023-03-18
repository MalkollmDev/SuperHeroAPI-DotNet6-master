using SuperHeroAPI.Models.DTO.LessonDTO;

namespace SuperHeroAPI.Models.DTO.Homework
{
    public class HomeworkDto
    {
        public int Id { get; set; }
        public string LessonName { get; set; }        
        public int NumberGroup { get; set; }        
        public string Task { get; set; }        
        public DateTime Date { get; set; }        
    }
}
