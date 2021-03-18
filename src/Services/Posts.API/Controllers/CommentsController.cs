using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Posts.API.Entities;

namespace Posts.API.Controllers
{
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly PostsDbContext _context;
        
        public CommentsController(PostsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Comments/Get/{id}")]
        public async Task<ActionResult<Comment>> Get(int id) 
            => await _context.Comment.FirstAsync(c => c.Id == id);

        [HttpGet]
        [Route("/Comments/GetByPost/{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetByPost(int postId)
            => await _context.Comment.Where(c => c.PostId == postId).ToListAsync();

        [HttpGet]
        [Route("/Comments/GetByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetByUser(int userId)
            => await _context.Comment.Where(c => c.UserId == userId).ToListAsync();

        [HttpPost]
        [Route("/Comments/Create")]
        public async Task Create(int userId, string text, int postId)
        {
            await _context.Comment.AddAsync(new Comment(userId, text, postId));
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Comments/Update")]
        public async Task Update(int id, string text)
        {
            var comment = await _context.Comment.FirstAsync(c => c.Id == id);
            comment.Text = text;
            await _context.SaveChangesAsync();
        }
        
        [HttpPost]
        [Route("/Comments/Delete")]
        public async Task Delete(int id)
        {
            var comment = await _context.Comment.FirstAsync(c => c.Id == id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}