using System.Threading.Tasks;
using Files.API.Entities;
using Files.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Files.API.Controllers
{
    [ApiController]
    public class AvatarsController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public AvatarsController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet]
        [Route("/Avatars/Get/{creatorId:int}")]
        public async Task<Avatar> Get(int creatorId)
        {
            return await _fileRepository.GetAvatarAsync(creatorId);
        }
    }
}