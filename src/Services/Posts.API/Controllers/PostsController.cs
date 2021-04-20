using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.API.Entities;

namespace Posts.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : Controller
    {
        private readonly PostsDbContext _context;
        
        public PostsController(PostsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Posts/Get")]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
            => await _context.Post.ToListAsync();
        
        [HttpGet]
        [Route("/Posts/Get/{id}")]
        public async Task<ActionResult<Post>> Get(int id)
            => await _context.Post.FirstAsync(p => p.Id == id);
        
        [HttpGet]
        [Route("/Posts/GetByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetByUser(int userId) 
            => await _context.Post.Where(p => p.UserId == userId).ToListAsync();

        [HttpGet]
        [Route("/Posts/GetByProject/{projectId}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetByProject(int projectId)
            => await _context.Post.Where(p => p.ProjectId == projectId).ToListAsync();
        
        [HttpGet]
        [Route("/Posts/GetByUserAndProject")]
        public async Task<ActionResult<IEnumerable<Post>>> GetByUserAndProject(int userId, int projectId)
            => await _context
                .Post
                .Where(p => p.UserId == userId && p.ProjectId == projectId)
                .ToListAsync();

        [HttpPost]
        [Route("/Posts/Create")]
        public async Task Create(Post post)
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
        }
        
        [HttpPost]
        [Route("/Posts/Update")]
        public async Task Update(int id, string text)
        {
            var post = await _context.Post.FirstAsync(p => p.Id == id);
            post.Text = text;
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Posts/Delete")]
        public async Task Delete(int id)
        {
            var post = await _context
                .Post
                .Include(p => p.Comments)
                .Include(p => p.Files)
                .FirstAsync(p => p.Id == id);
            post.Comments.ForEach(c => _context.Comment.Remove(c));
            post.Files.ForEach(f => _context.File.Remove(f));
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}