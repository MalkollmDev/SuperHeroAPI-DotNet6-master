namespace SuperHeroAPI.Models
{
    public class New
    {
        public int Id { get; set; }        
        public string Title { get; set; }
        public string Preface { get; set; }
        public string Text { get; set; }
        public bool isPublished { get; set; }
        public DateTime published { get; set; }
    }
}
