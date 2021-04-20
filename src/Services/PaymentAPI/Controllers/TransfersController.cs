using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    public class TransfersController : Controller
    {
        private readonly PaymentContext _context;
        public TransfersController(PaymentContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Transfers/Get")]
        public ActionResult<IEnumerable<Transfer>> Get()
            => _context.Transfers.ToList();

        [HttpGet]
        [Route("/Transfers/GetByUsersFrom/{userId}")]
        public ActionResult<IEnumerable<Transfer>> GetByUsersFrom(int userId)
            => _context.Transfers.Where(c => c.UserFrom == userId).ToList();


        [HttpGet]
        [Route("/Transfers/GetByUserTo/{userId}")]
        public ActionResult<IEnumerable<Transfer>> GetByUserTo(int userId)
            => _context.Transfers.Where(c => c.UserTo == userId).ToList();

        //TODO: Нужно ли удаление??? 
        //[HttpPost]
        //[Route("/Transfers/Delete")]
        //public async Task Delete(int userId)
        //{
        //    var bankAccount = await _context.Transfers.FirstAsync(c => c.UserId == userId);
        //    _context.Transfers.Remove(bankAccount);
        //    await _context.SaveChangesAsync();
        //}

        [HttpPost]
        [Route("/Transfers/Add")]
        public async Task Add([FromBody]Transfer newTransfer)
        {
            newTransfer.DateTime = DateTime.Now;
            _context.Transfers.Add(newTransfer);
            await _context.SaveChangesAsync();
        }

       
    }
}
