using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Developer.API.DTOs;
using Developer.API.Entities;
using Developer.API.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Developer.API.Controllers
{
    [ApiController]
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
        {
            var companies = _context.Company.ToList();
            return companies.Count == 0 ? null : companies;
        }

        [HttpGet]
        [Route("/Companies/Get/{id:int}")]
        public ActionResult<Company> Get(int id)
            => _context.Company.FirstOrDefault(c => c.Id == id);

        [HttpGet]
        [Route("/Companies/Get/{name}")]
        public ActionResult<Company> Get(string name)
            => _context.Company.FirstOrDefault(c => c.Name == name);

        [HttpGet]
        [Route("/Companies/GetByUser/{userId:int}")]
        public ActionResult<IEnumerable<Company>> GetByUser(int userId)
            => _context
                .User
                .Where(u => u.Id == userId)
                .Select(u => u.Companies)
                .FirstOrDefault();

        [HttpGet]
        [Route("/Companies/GetByProject/{projectId:int}")]
        public ActionResult<Company> GetByProject(int projectId)
            => _context
                .Project
                .Where(p => p.Id == projectId)
                .Select(p => p.Company)
                .FirstOrDefault();

        [HttpGet]
        [Route("/Companies/GetByName/{name}")]
        public ActionResult<IEnumerable<Company>> GetByName(string name)
        {
            var companies = _context
                .Company
                .Where(c => c.Name.ToLower().Contains(name.ToLower()))
                .ToList();
            return companies.Count == 0 ? null : companies;
        }
        
        [HttpPost]
        [Route("/Companies/Create")]
        public ActionResult<string> Create(CompanyForm companyForm)
        {
            return Company.Create(companyForm);
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
        [Route("/Companies/AddProject")]
        public async Task AddProject(int companyId, int projectId)
        {
            var company = await _context.Company.FirstAsync(c => c.Id == companyId);
            var project = await _context.Project.FirstAsync(p => p.Id == projectId);
            company.Projects.Add(project);
        }

        [HttpPost]
        [Route("/Companies/Update")]
        public async Task Update(Company company)
        {
            var updateCompany = await _context.Company.FirstAsync(c => c.Id == company.Id);
            updateCompany.Name = company.Name;
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Companies/AddUser")]
        public async Task AddUser(AddUserDto dto)
        {
            var company = await _context
                .Company
                .Include(c => c.Users)
                .FirstAsync(c => c.Id == dto.ProjectOrCompanyId);
            var user = await _context.User.FirstAsync(u => u.Id == dto.UserId);
            company.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}