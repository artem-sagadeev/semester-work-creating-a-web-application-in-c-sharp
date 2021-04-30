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
        {
            var posts = await _context
                .Post
                .ToListAsync();
            return posts.Count == 0 ? null : posts;
        }
        
        [HttpGet]
        [Route("/Posts/Get/{id:int}")]
        public async Task<ActionResult<Post>> Get(int id)
            =>  await _context.Post.FirstOrDefaultAsync(p => p.Id == id);
        
        [HttpGet]
        [Route("/Posts/GetByUser/{userId:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetByUser(int userId)
        {
            var posts = await _context
                .Post
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return posts.Count == 0 ? null : posts;
        }

        [HttpGet]
        [Route("/Posts/GetByProject/{projectId:int}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetByProject(int projectId)
        {
            var posts = await _context
                .Post
                .Where(p => p.ProjectId == projectId)
                .ToListAsync();
            return posts.Count == 0 ? null : posts;
        }

        [HttpGet]
        [Route("/Posts/GetByUserAndProject")]
        public async Task<ActionResult<IEnumerable<Post>>> GetByUserAndProject(int userId, int projectId)
        {
            var posts = await _context
                .Post
                .Where(p => p.UserId == userId && p.ProjectId == projectId)
                .ToListAsync();
            return posts.Count == 0 ? null : posts;
        }

        [HttpPost]
        [Route("/Posts/Create")]
        public async Task Create(Post post)
        {
            //todo not checked
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
        }
        
        [HttpPost]
        [Route("/Posts/Update")]
        public async Task Update(int id, string text)
        {
            //todo not checked
            var post = await _context.Post.FirstAsync(p => p.Id == id);
            post.Text = text;
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Posts/Delete")]
        public async Task Delete(int id)
        {
            //todo not checked
            var post = await _context
                .Post
                .Include(p => p.Comments)
                .FirstAsync(p => p.Id == id);
            post.Comments.ForEach(c => _context.Comment.Remove(c));
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}