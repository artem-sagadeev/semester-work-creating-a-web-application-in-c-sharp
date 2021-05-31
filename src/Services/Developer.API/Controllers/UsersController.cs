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
        {
            var users = _context
                .User
                .ToList();
            return users.Count == 0 ? null : users;
        }

        [HttpGet]
        [Route("/Users/Get/{id:int}")]
        public ActionResult<User> Get(int id)
            => _context
                .User
                .FirstOrDefault(u => u.Id == id);

        [HttpGet]
        [Route("/Users/GetByCompany/{companyId:int}")]
        public ActionResult<IEnumerable<User>> GetByCompany(int companyId)
            => _context
                .Company
                .Where(c => c.Id == companyId)
                .Select(c => c.Users)
                .FirstOrDefault();

        [HttpGet]
        [Route("/Users/GetByProject/{projectId:int}")]
        public ActionResult<IEnumerable<User>> GetByProject(int projectId)
            => _context
                .Project
                .Where(p => p.Id == projectId)
                .Select(p => p.Users)
                .FirstOrDefault();

        [HttpGet]
        [Route("/Users/GetByName/{name}")]
        public ActionResult<IEnumerable<User>> GetByName(string name)
        {
            var users = _context
                .User
                .Where(u => u.Name.ToLower().Contains(name.ToLower()))
                .ToList();
            return users.Count == 0 ? null : users;
        }

        [HttpPost]
        [Route("/Users/Create")]
        public async Task Create(User user)
        {
            //todo not checked
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Users/Delete")]
        public async Task Delete(int id)
        {
            //todo not checked
            var user = await _context.User.FirstAsync(u => u.Id == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Users/Update")]
        public async Task Update(User user)
        {
            var updateUser = _context.User.First(u => u.Id == user.Id);
            updateUser.Name = user.Name;
            await _context.SaveChangesAsync();
        } 
    }
}