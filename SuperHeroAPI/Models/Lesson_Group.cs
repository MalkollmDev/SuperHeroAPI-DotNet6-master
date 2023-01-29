﻿namespace SuperHeroAPI.Models
{
    public class Lesson_Group
    {
        public int Id { get; set; }        
        public int LessonId { get; set; }
        public int GroupId { get; set; }
        public int TeacherId { get; set; }        
        public Lesson Lessons { get; set; }
        public Group Groups { get; set; }
        public Teacher Teachers { get; set; }
        public LessonTime LessonTimes { get; set; }
    }
}
