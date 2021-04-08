using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Developer.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Developer.API.Controllers
{
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DeveloperDbContext _context;

        public UsersController(DeveloperDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Users/Get")]
        public ActionResult<IEnumerable<User>> Get()
            => _context
                .User
                .ToList();
        
        [HttpGet]
        [Route("/Users/Get/{id}")]
        public ActionResult<User> Get(int id)
            => _context
                .User
                .First(u => u.Id == id);

        [HttpGet]
        [Route("/Users/GetByCompany/{companyId}")]
        public ActionResult<IEnumerable<User>> GetByCompany(int companyId)
            => _context
                .Company
                .Where(c => c.Id == companyId)
                .Select(c => c.Users)
                .First();

        [HttpGet]
        [Route("/Users/GetByProject/{projectId}")]
        public ActionResult<IEnumerable<User>> GetByProject(int projectId)
            => _context
                .Project
                .Where(p => p.Id == projectId)
                .Select(p => p.Users)
                .First();

        [HttpGet]
        [Route("/Users/GetByName/{name}")]
        public ActionResult<IEnumerable<User>> GetByName(string name)
            => _context
                .User
                .Where(u => u.Name.Contains(name))
                .ToList();

        [HttpPost]
        [Route("/Users/Create")]
        public async Task Create(string login)
        {
            await _context.User.AddAsync(new User(login));
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Users/Delete")]
        public async Task Delete(int id)
        {
            var user = await _context.User.FirstAsync(u => u.Id == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}