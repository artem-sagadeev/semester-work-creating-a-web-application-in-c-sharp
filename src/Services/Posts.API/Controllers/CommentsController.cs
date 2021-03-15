using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Posts.API.Entities;

namespace Posts.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : Controller
    {
        private readonly PostsDbContext _context;
        
        public CommentsController(PostsDbContext context)
        {
            _context = context;
        }
    }
}