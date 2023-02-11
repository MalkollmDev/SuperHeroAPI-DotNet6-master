using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly DataContext _context;

        public FileController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var file = _context.Files.FirstOrDefault(F => F.Id == id);
            if (file == null)
            {
                return NotFound();
            }

            var extension = file.Extension.Replace(".", "");

            return new FileContentResult(file.Data, $"file/{extension}");
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] FileUploadDTO fileDTO)
        {
            MemoryStream stream = new MemoryStream();
            fileDTO.file.CopyTo(stream);

            int? temp;
            if (fileDTO.EventID == 0)
            {
                temp = null;
            }
            else
            {
                temp = fileDTO.EventID;
            }

            var file = new Models.File
            {
                EventId = temp,
                Data = stream.GetBuffer(),
                OriginalName = fileDTO.file.FileName.Split(".")[0],
                Extension = fileDTO.file.FileName.Split(".")[1],
            };

            _context.Files.Add(file);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
