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
        public ActionResult<IEnumerable<Project>> Get()
            => _context.Project.ToList();
        
        [HttpGet]
        [Route("/Projects/Get/{id}")]
        public ActionResult<Project> Get(int id)
            => _context.Project.First(p => p.Id == id);

        [HttpGet]
        [Route("/Projects/GetByCompany/{companyId}")]
        public ActionResult<IEnumerable<Project>> GetByCompany(int companyId)
            => _context
                .Company
                .Where(c => c.Id == companyId)
                .Select(c => c.Projects)
                .First();

        [HttpGet]
        [Route("/Projects/GetByUser/{userId}")]
        public ActionResult<IEnumerable<Project>> GetByUser(int userId)
            => _context
                .User
                .Where(u => u.Id == userId)
                .Select(u => u.Projects)
                .First();

        [HttpGet]
        [Route("/Projects/GetByName/{name}")]
        public ActionResult<IEnumerable<Project>> GetByName(string name)
            => _context
                .Project
                .Where(p => p.Name.Contains(name))
                .ToList();

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