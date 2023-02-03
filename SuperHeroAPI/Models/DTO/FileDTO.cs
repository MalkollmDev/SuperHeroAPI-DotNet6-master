namespace SuperHeroAPI.Models.DTO
{
    public class FileDTO
    {
        public Guid Id { get; set; }
        public string Extension { get; set; }
        public string DownloadUrl{ get; set; }
    }
}
