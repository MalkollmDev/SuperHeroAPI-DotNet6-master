using Microsoft.AspNetCore.Http;
namespace SuperHeroAPI.Models
{
    public class NewViewModel
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        public IFormFile? File { get; set; }
    }
}
