using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Developer.API.DTOs;
using Developer.API.Entities;
using Developer.API.Forms;
using Microsoft.AspNetCore.Identity;
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
        {
            var projects = _context.Project.ToList();
            return projects.Count == 0 ? null : projects;
        }
        
        [HttpGet]
        [Route("/Projects/Get/{id:int}")]
        public ActionResult<Project> Get(int id)
            => _context.Project.FirstOrDefault(p => p.Id == id);

        [HttpGet]
        [Route("/Projects/Get/{name}")]
        public ActionResult<Project> Get(string name)
            => _context.Project.FirstOrDefault(p => p.Name == name);

        [HttpGet]
        [Route("/Projects/GetByCompany/{companyId:int}")]
        public ActionResult<IEnumerable<Project>> GetByCompany(int companyId)
            => _context
                .Company
                .Where(c => c.Id == companyId)
                .Select(c => c.Projects)
                .FirstOrDefault();

        [HttpGet]
        [Route("/Projects/GetByUser/{userId:int}")]
        public ActionResult<IEnumerable<Project>> GetByUser(int userId)
            => _context
                .User
                .Where(u => u.Id == userId)
                .Select(u => u.Projects)
                .FirstOrDefault();

        [HttpGet]
        [Route("/Projects/GetByName/{name}")]
        public ActionResult<IEnumerable<Project>> GetByName(string name)
        {
            var projects = _context
                .Project
                .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                .ToList();
            return projects.Count == 0 ? null : projects;
        }

        [HttpPost]
        [Route("/Projects/Create")]
        public ActionResult<string> Create(ProjectForm projectForm)
        {
            return Project.Create(projectForm);
        }
        
        [HttpPost]
        [Route("/Projects/Delete")]
        public async Task Delete(int id)
        {
            var project = await _context.Project.FirstAsync(p => p.Id == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Projects/Update")]
        public async Task Update(Project project)
        {
            var updateProject = await _context.Project.FirstAsync(p => p.Id == project.Id);
            updateProject.Name = project.Name;
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Projects/AddUser")]
        public async Task AddUser(AddUserDto dto)
        {
            var updateProject = await _context
                .Project
                .Include(p => p.Users)
                .FirstAsync(p => p.Id == dto.ProjectOrCompanyId);
            var user = await _context.User.FirstAsync(u => u.Id == dto.UserId);
            updateProject.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}