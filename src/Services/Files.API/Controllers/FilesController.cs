using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        [Route("/Files/GetFile/{id}")]
        public async Task<File> GetFile(string id)
        {
            return await _fileRepository.GetFileAsync(id);
        }
        
        [HttpGet]
        [Route("/Files/GetPostFiles/{postId:int}")]
        public async Task<IEnumerable<File>> GetPostFiles(int postId)
        {
            return await _fileRepository.GetPostFiles(postId);
        }

        [HttpPost]
        [Route("/Files/Create/{postId:int}")]
        public async Task Create(int postId, IFormFile uploadedFile)
        {
            if (uploadedFile == null) return;
            var file = new File(postId, uploadedFile.FileName, uploadedFile.ContentType);
            await using (var fileStream = new FileStream(file.Path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            await _fileRepository.CreateFileAsync(file);
        }
        
        [HttpPost]
        [Route("/Files/Delete/{id}")]
        public async Task Delete(string id)
        {
            await _fileRepository.DeleteFileAsync(id);
        }
    }
}