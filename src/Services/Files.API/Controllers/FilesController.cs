using System.IO;
using System.Threading.Tasks;
using Files.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FileInfo = Files.API.Entities.FileInfo;

namespace Files.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public FilesController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<FileResult> GetFileAsync(string id)
        {
            var file = await _fileRepository.GetFileAsync(id);
            
            var bytes = await System.IO.File.ReadAllBytesAsync(file.Path);
            return File(bytes, file.Type, file.Name);
        }

        [HttpPost]
        public async Task CreateFileAsync(IFormFile uploadedFile)
        {
            if (uploadedFile == null) return;
            var file = new FileInfo(uploadedFile.FileName, uploadedFile.ContentType);
            await using (var fileStream = new FileStream(file.Path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            await _fileRepository.CreateFileAsync(file);
        }
        
        [HttpPost]
        public async Task DeleteFileAsync(string id)
        {
            await _fileRepository.DeleteFileAsync(id);
        }
    }
}