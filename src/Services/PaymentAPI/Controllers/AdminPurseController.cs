using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PaymentAPI.Controllers
{
    public class AdminPurseController : Controller
    {
        private readonly PaymentContext _context;
        public AdminPurseController(PaymentContext context)
        {
            _context = context;
        }

        public class MoneyFromat
        {
            public int money { get; set; }
        }

        [HttpPost]
        [Route("/AdminPurse/TransferMoney")]
        public async Task TransferMoney([FromBody]MoneyFromat money)
        {
        }
    }
}