using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Models;
using SuperHeroAPI.Models.DTO;

namespace SuperHeroAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DataContext _context;

        public DocumentsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Document>>> Get()
        {
            var models = await _context.Documents.ToListAsync();

            var documents = new List<DocumentsDTO>();

            

            foreach (var model in models)
            {
                var file = _context.Files.FirstOrDefault(F => F.Id == model.FileId);
                var d = new DocumentsDTO
                {
                    DownloadUrl = $"{Request.Scheme}://{Request.Host}/File/{file.Id}",
                    OriginalName = file.OriginalName,
                    IsShow = model.IsShow
                };

                documents.Add(d);
            }

            return Ok(documents);
        }

        [HttpPost]
        public async Task<ActionResult> Post(IFormCollection files)
        {
            int? _eventId = null; //заглушка, надо убрать EventId из таблицы с файлами
            var file = new Models.File();
            foreach (var item in files.Files)
            {
                var fileNameParts = item.FileName.Split('.');
                var stack = new Stack<string>(fileNameParts);

                using (var stream = new MemoryStream())
                {
                    item.CopyTo(stream);

                    file = new Models.File
                    {
                        Extension = stack.Pop(),
                        OriginalName = string.Join(".", stack.ToArray().Reverse()),
                        EventId = _eventId,
                        Data = stream.GetBuffer()
                    };

                    _context.Files.Add(file);
                    await _context.SaveChangesAsync();
                }
            }

            var model = new Document
            {
                FileId = file.Id,
                IsShow = true
            };

            _context.Documents.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        //[HttpGet("GetImageUrl/{id}")]
        //public async Task<ActionResult<string>> GetImageUrl(Guid id)
        //{
        //    var DownloadUrl = $"{Request.Scheme}://{Request.Host}/File/{id}";

        //    return DownloadUrl;
        //}


    }
}
