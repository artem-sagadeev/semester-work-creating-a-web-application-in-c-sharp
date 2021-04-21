using System.Collections.Generic;
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
        
        [HttpGet]
        [Route("/Files/GetPostFiles/{postId:int}")]
        public async Task<IEnumerable<File>> GetPostFiles(int postId)
        {
            return await _fileRepository.GetPostFiles(postId);
        }

        [HttpPost]
        [Route("/Files/Create/{postId}")]
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