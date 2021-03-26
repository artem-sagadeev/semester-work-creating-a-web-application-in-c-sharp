using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Controllers
{
    public class WithdrawalsController : Controller
    {
        private readonly PaymentContext _context;
        public WithdrawalsController(PaymentContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Withdrawals/Get")]
        public ActionResult<IEnumerable<Withdrawal>> Get()
            => _context.Withdrawals.ToList();

        [HttpGet]
        [Route("/Withdrawals/Get/{userId}")]
        public ActionResult<IEnumerable<Withdrawal>> Get(int userId)
            => _context.Withdrawals.Where(c => c.UserID == userId).ToList();


        //TODO: Сделать ли удаление доступным?
        //[HttpPost]
        //[Route("/Withdrawals/Delete")]
        //public async Task Delete(int userId)
        //{
        //    var purse = await _context.Withdrawals.FirstAsync(c => c.user == userId);
        //    _context.VirtualPurses.Remove(purse);
        //    await _context.SaveChangesAsync();
        //}

        [HttpPost]
        [Route("/Withdrawals/Add")]
        public async Task Add(int userId, int sum)
        {
            var newWithdrawal = new Withdrawal()
            {
                UserID = userId,
                Sum = sum,
                DateTime = DateTime.Now
            };
            _context.Withdrawals.Add(newWithdrawal);
            await _context.SaveChangesAsync();
        }
    }
}
