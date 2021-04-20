using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PaymentAPI.Controllers
{
    public class StorageOfMoneyController : Controller
    {
        private readonly PaymentContext _context;
        public StorageOfMoneyController(PaymentContext context)
        {
            _context = context;
        }
        
        public class MoneyFromat
        {
            private int money { get; set; }
        }
        
        [HttpPost]
        [Route("/StorageOfMoney/TransferMoney")]
        public async Task TransferMoney([FromBody] BankAccount bankAccount)
        {
        }
        
        [HttpPost]
        [Route("/StorageOfMoney/AddMoney")]
        public async Task AddMoney([FromBody]MoneyFromat money)
        {
        }
    }
}