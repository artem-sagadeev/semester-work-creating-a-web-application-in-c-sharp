using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Developer.API.Entities;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Company>> Get(int id)
            => await _context.Company.FirstAsync(c => c.Id == id);

        [HttpGet]
        [Route("/Companies/GetByUser")]
        public async Task<ActionResult<IEnumerable<Company>>> GetByUser(int userId)
            => (await _context
                    .User
                    .Include(u => u.Companies)
                    .FirstAsync(u => u.Id == userId))
                .Companies;


        [HttpPost]
        [Route("/Companies/Create")]
        public async Task Create(string name, int ownerId)
        {
            await _context.Company.AddAsync(new Company(name, ownerId));
            await _context.SaveChangesAsync();
        }

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