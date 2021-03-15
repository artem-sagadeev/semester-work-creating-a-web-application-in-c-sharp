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
        
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUser(int userId) 
            => await _context.Post.Where(p => p.UserId == userId).ToListAsync();

        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByGroup(int groupId)
            => await _context.Post.Where(p => p.GroupId == groupId).ToListAsync();
        
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUserAndGroup(int userId, int groupId)
            => await _context
                .Post
                .Where(p => p.UserId == userId && p.GroupId == groupId)
                .ToListAsync();
    }
}