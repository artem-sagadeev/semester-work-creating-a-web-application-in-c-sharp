using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    public class VirtualPursesController : Controller
    {
        private readonly PaymentContext _context;
        public VirtualPursesController(PaymentContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/VirtualPurses/Get")]
        public ActionResult<IEnumerable<VirtualPurse>> Get()
            => _context.VirtualPurses.ToList();

        [HttpGet]
        [Route("/VirtualPurses/Get/{userId}")]
        public ActionResult<VirtualPurse> Get(int userId)
            => _context.VirtualPurses.First(c => c.UserId == userId);


        [HttpPost]
        [Route("/VirtualPurses/UpdateMoney")]
        public async Task UpdateMoney(int userId, int money)
        {

            var purse = await _context.VirtualPurses.FirstAsync(c => c.UserId == userId);
            purse.Money = money;
            await _context.SaveChangesAsync();
        }


        [HttpPost]
        [Route("/VirtualPurses/Delete")]
        public async Task Delete(int userId)
        {
            var purse = await _context.VirtualPurses.FirstAsync(c => c.UserId == userId);
            _context.VirtualPurses.Remove(purse);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/VirtualPurses/Add")]
        public async Task Add(int userId, int money)
        {
            var newVirtualPurse = new VirtualPurse()
            {
                UserId = userId,
                Money = money
            };
            _context.VirtualPurses.Add(newVirtualPurse);
            await _context.SaveChangesAsync();
        }
    }
}
