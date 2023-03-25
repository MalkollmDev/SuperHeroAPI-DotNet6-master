using SuperHeroAPI.Models.DTO.LessonDTO;

namespace SuperHeroAPI.Models.DTO.ScheduleDTO
{
    public class ScheduleDto
    {
        public string Date { get; set; }
        public List<LessonDto> Lessons { get; set; }
    }
}
