namespace SuperHeroAPI.Models.DTO
{
    public class EventDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public DateTime Published { get; set; }
        public List<File> Files { get; set; }
    }
}
