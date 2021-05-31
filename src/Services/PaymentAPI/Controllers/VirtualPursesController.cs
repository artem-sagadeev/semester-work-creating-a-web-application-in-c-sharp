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
        {
            var x = _context.VirtualPurses.ToList();
           return x.First(c => c.UserId == userId);
        }
            

        public class UserIdMoneyIdFormat
        {
            public int userId { get; set; }
            public int money { get; set; }
        }

        [HttpPost]
        [Route("/VirtualPurses/UpdateMoney")]
        public async Task UpdateMoney([FromBody]UserIdMoneyIdFormat userIdMoneyIdFormat)
        {
            var purse = await _context.VirtualPurses.FirstAsync(c => c.UserId == userIdMoneyIdFormat.userId);
            purse.Money = userIdMoneyIdFormat.money;
            await _context.SaveChangesAsync();
        }


        public class UserIdFormat
        {
            public int userId { get; set; }
        }
        [HttpPost]
        [Route("/VirtualPurses/Delete")]
        public async Task Delete([FromBody]UserIdFormat userIdFormat)
        {
            var purse = await _context.VirtualPurses.FirstAsync(c => c.UserId == userIdFormat.userId);
            _context.VirtualPurses.Remove(purse);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/VirtualPurses/Add")]
        public async Task Add([FromBody]VirtualPurse newVirtualPurse)
        {
           _context.VirtualPurses.Add(newVirtualPurse);
            await _context.SaveChangesAsync();
        }
    }
}
