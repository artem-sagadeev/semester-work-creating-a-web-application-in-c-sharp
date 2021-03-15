using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.API.Entities;

namespace Posts.API.Controllers
{
    [ApiController]
    public class FilesController : Controller
    {
        private readonly PostsDbContext _context;
        
        public FilesController(PostsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Files/Get")]
        public async Task<ActionResult<File>> Get(int id)
            => await _context.File.FirstAsync(f => f.Id == id);

        [HttpGet]
        [Route("/Files/GetByPostId")]
        public async Task<ActionResult<IEnumerable<File>>> GetByPostId(int postId)
            => await _context.File.Where(f => f.PostId == postId).ToListAsync();

        [HttpPost]
        [Route("/Files/Create")]
        public async Task Create(string fileId, string contentType, int postId)
        {
            await _context.File.AddAsync(new File(fileId, contentType, postId));
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Files/Delete")]
        public async Task Delete(int id)
        {
            var file = await _context.File.FirstAsync(f => f.Id == id);
            _context.File.Remove(file);
            await _context.SaveChangesAsync();
        }
    }
}