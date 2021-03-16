using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Developer.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Developer.API.Controllers
{
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly DeveloperDbContext _context;

        public ProjectsController(DeveloperDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Projects/Get")]
        public async Task<ActionResult<Project>> Get(int id)
            => await _context.Project.FirstAsync(p => p.Id == id);

        [HttpGet]
        [Route("/Projects/GetByCompany")]
        public async Task<ActionResult<IEnumerable<Project>>> GetByCompany(int companyId)
            => await _context.Project.Where(p => p.CompanyId == companyId).ToListAsync();

        [HttpGet]
        [Route("/Projects/GetByUser")]
        public async Task<ActionResult<IEnumerable<Project>>> GetByUser(int userId)
            => (await _context
                    .User
                    .Include(u => u.Projects)
                    .FirstAsync(u => u.Id == userId))
                .Projects;

        [HttpPost]
        [Route("/Projects/Create")]
        public async Task Create(string name, int ownerId)
        {
            await _context.Project.AddAsync(new Project(name, ownerId));
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Projects/Delete")]
        public async Task Delete(int id)
        {
            var project = await _context.Project.FirstAsync(p => p.Id == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}