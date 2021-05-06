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
        [Route("/Files/Get/{id}")]
        public async Task<File> Get(string id)
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
        [Route("/Files/Create")]
        public async Task Create(File file)
        {
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