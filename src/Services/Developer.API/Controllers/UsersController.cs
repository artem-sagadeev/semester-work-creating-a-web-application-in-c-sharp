using System.Collections.Generic;
using System.Data.Entity;
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
        public async Task<ActionResult<User>> Get(int id)
            => await _context.User.FirstAsync(u => u.Id == id);

        [HttpGet]
        [Route("/Users/GetByCompany")]
        public async Task<ActionResult<IEnumerable<User>>> GetByCompany(int companyId)
            => (await _context
                    .Company
                    .Include(c => c.Users)
                    .FirstAsync(c => c.Id == companyId))
                .Users;

        [HttpGet]
        [Route("/Users/GetByProject")]
        public async Task<ActionResult<IEnumerable<User>>> GetByProject(int projectId)
            => (await _context
                    .Project
                    .Include(p => p.Users)
                    .FirstAsync(p => p.Id == projectId))
                .Users;

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