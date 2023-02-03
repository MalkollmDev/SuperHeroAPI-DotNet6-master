namespace SuperHeroAPI.Models.DTO
{
    public class FileUploadDTO
    {
        public int EventID { get; set; }
        public IFormFile file { get; set; }
    }
}
