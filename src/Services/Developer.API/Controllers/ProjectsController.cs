using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Developer.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("/Projects/GetByUser/{userId}")]
        public ActionResult<IEnumerable<Project>> GetByUser(int userId)
            => _context
                .User
                .Where(u => u.Id == userId)
                .Select(u => u.Projects)
                .First();

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