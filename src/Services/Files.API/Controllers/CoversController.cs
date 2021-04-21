using System.Threading.Tasks;
using Files.API.Entities;
using Files.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Files.API.Controllers
{
    [ApiController]
    public class CoversController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public CoversController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet]
        [Route("/Covers/Get/{postId:int}")]
        public async Task<Cover> Get(int postId)
        {
            return await _fileRepository.GetCoverAsync(postId);
        }
    }
}