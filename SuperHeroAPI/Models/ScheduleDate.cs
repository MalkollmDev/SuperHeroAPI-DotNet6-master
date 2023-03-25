using SuperHeroAPI.Models.DTO.LessonDTO;
using System.Numerics;

namespace SuperHeroAPI.Models
{
    public class ScheduleDate
    {
        public int Id { get; set; }        
        public DateTime Date { get; set; }
        public int LessonGroupId { get; set; }
        public LessonGroup LessonGroups { get; set; }
    }
}