using System.IO;
using System.Threading.Tasks;
using Files.API.Entities;
using Files.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using File = Files.API.Entities.File;

namespace Files.API.Controllers
{
    [ApiController]
    public class FilesController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public FilesController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        
        [HttpGet]
        [Route("/Files/GetLink/{id}")]
        public async Task<ActionResult<string>> GetLink(string id)
        {
            var link = new Link(id);
            await _fileRepository.CreateLinkAsync(link);
            return link.Uri;
        }
        
        [HttpGet]
        [Route("/Files/GetFile/{id}/{token}")]
        public async Task<ActionResult> GetFile(string id, string token)
        {
            var link = await _fileRepository.GetLinkAsync(id, token);

            if (link.Token != token)
                return Forbid();
            var file = await _fileRepository.GetFileAsync(id);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            var bytes = await System.IO.File.ReadAllBytesAsync(file.Path);

            await _fileRepository.DeleteLinkAsync(link.Id);
            return File(bytes, file.Type, file.Name);
        }

        [HttpPost]
        [Route("/Files/Create")]
        public async Task Create(IFormFile uploadedFile)
        {
            if (uploadedFile == null) return;
            var file = new File(uploadedFile.FileName, uploadedFile.ContentType);
            await using (var fileStream = new FileStream(file.Path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            await _fileRepository.CreateFileAsync(file);
        }
        
        [HttpPost]
        [Route("/Files/Delete")]
        public async Task Delete(string id)
        {
            await _fileRepository.DeleteFileAsync(id);
        }
    }
}