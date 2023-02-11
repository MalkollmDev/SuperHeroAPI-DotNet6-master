namespace SuperHeroAPI.Models
{
    public class File
    {
        public Guid Id { get; set; }
        public byte[] Data { get; set; }
        public string OriginalName { get; set; }
        public string Extension { get; set; }
        public int? EventId { get; set; }
    }
}
