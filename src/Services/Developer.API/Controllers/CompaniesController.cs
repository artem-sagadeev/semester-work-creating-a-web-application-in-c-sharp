using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Developer.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Developer.API.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly DeveloperDbContext _context;

        public CompaniesController(DeveloperDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Companies/Get")]
        public ActionResult<IEnumerable<Company>> Get()
            => _context.Company.ToList();
        
        [HttpGet]
        [Route("/Companies/Get/{id}")]
        public ActionResult<Company> Get(int id)
            => _context.Company.First(c => c.Id == id);

        [HttpGet]
        [Route("/Companies/GetByUser/{userId}")]
        public ActionResult<IEnumerable<Company>> GetByUser(int userId)
            => _context
                .User
                .Where(u => u.Id == userId)
                .Select(u => u.Companies)
                .First();

        [HttpGet]
        [Route("/Companies/GetByProject/{projectId}")]
        public ActionResult<Company> GetByProject(int projectId)
            => _context
                .Project
                .Where(p => p.Id == projectId)
                .Select(p => p.Company)
                .First();

        [HttpGet]
        [Route("/Companies/GetByName/{name}")]
        public ActionResult<IEnumerable<Company>> GetByName(string name)
            => _context
                .Company
                .Where(c => c.Name.Contains(name))
                .ToList();
        
        [HttpPost]
        [Route("/Companies/Delete")]
        public async Task Delete(int id)
        {
            var company = await _context.Company.FirstAsync(c => c.Id == id);
            _context.Company.Remove(company);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Companies/AddUser")]
        public async Task AddUser(int companyId, int userId)
        {
            var company = await _context.Company.FirstAsync(c => c.Id == companyId);
            var user = await _context.User.FirstAsync(u => u.Id == userId);
            company.Users.Add(user);
        }

        public async Task AddProject(int companyId, int projectId)
        {
            var company = await _context.Company.FirstAsync(c => c.Id == companyId);
            var project = await _context.Project.FirstAsync(p => p.Id == projectId);
            company.Projects.Add(project);
        }
    }
}