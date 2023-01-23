namespace SuperHeroAPI.Models
{
    public class Event
    {
        public int Id { get; set; }        
        public string Title { get; set; }
        public string Content { get; set; }
        public bool isPublished { get; set; }
        public DateTime published { get; set; }
    }
}
