using System.Collections.Generic;
using System.Linq;
using Developer.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Developer.API.Controllers
{
    [ApiController]
    public class TagsController : Controller
    {
        private readonly DeveloperDbContext _context;

        public TagsController(DeveloperDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Tags/GetByUser/{userId:int}")]
        public ActionResult<IEnumerable<Tag>> GetByUser(int userId)
            => _context
                .User
                .Where(u => u.Id == userId)
                .Select(u => u.Tags)
                .FirstOrDefault();
        
        [HttpGet]
        [Route("/Tags/GetByProject/{projectId:int}")]
        public ActionResult<IEnumerable<Tag>> GetByProject(int projectId)
            => _context
                .Project
                .Where(p => p.Id == projectId)
                .Select(p => p.Tags)
                .FirstOrDefault();
        
        [HttpGet]
        [Route("/Tags/GetByCompany/{companyId:int}")]
        public ActionResult<IEnumerable<Tag>> GetByCompany(int companyId)
            => _context
                .Company
                .Where(c => c.Id == companyId)
                .Select(c => c.Tags)
                .FirstOrDefault();
    }
}