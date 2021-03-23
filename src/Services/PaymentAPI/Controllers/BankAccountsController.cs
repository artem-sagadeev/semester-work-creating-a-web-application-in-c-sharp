using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    public class BankAccountsController : Controller
    {
        private readonly PaymentContext _context;
        public BankAccountsController(PaymentContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/BankAccounts/Get")]
        public ActionResult<IEnumerable<BankAccount>> Get()
            => _context.BankAccounts.ToList();

        [HttpGet]
        [Route("/BankAccounts/GetByUser/{userId}")]
        public ActionResult<BankAccount> Get(int userId)
            => _context.BankAccounts.First(c => c.UserId == userId);

        [HttpPost]
        [Route("/BankAccounts/Delete")]
        public async Task Delete(int userId)
        {
            var bankAccount = await _context.BankAccounts.FirstAsync(c => c.UserId == userId);
            _context.BankAccounts.Remove(bankAccount);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/BankAccounts/AddBankAccount")]
        public async Task AddBankAccount(int number, int userId)
        {            
            var newBankAccount = new BankAccount()
            {
                Number = number,
                UserId = userId
            };
            _context.BankAccounts.Add(newBankAccount);
            await _context.SaveChangesAsync();
        }

           }
}
