using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubscriptionAPI.Entities;

namespace SubscriptionAPI.Controllers
{
    public class PaidSubscriptionController : Controller
    {
        private readonly SubscriptionContext _context;
        public PaidSubscriptionController(SubscriptionContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/PaidSubscriptions/Get")]
        public ActionResult<IEnumerable<PaidSubscription>> Get()
        {
            var temp = _context.PaidSubscriptions.ToList();
            foreach (var t in temp)
            {
                _context.Entry(t).Reference(x => x.Tariff).Load();
            }
            return temp;
        }

        [HttpGet]
        [Route("/PaidSubscriptions/GetByUser/{userId}")]
        public ActionResult<IEnumerable<PaidSubscription>> GetByUser(int userId)
        {
            var temp = _context.PaidSubscriptions.Where(c => c.UserId == userId).ToList();
            foreach (var t in temp)
            {
                _context.Entry(t).Reference(x => x.Tariff).Load();
            }
            return temp;
        }

        [HttpGet]
        [Route("/PaidSubscriptions/GetBySubscribedToId/{subscribedToId}")]
        public ActionResult<IEnumerable<PaidSubscription>> GetBySubscribedToId(int subscribedToId)
        {
            var temp = _context.PaidSubscriptions.Where(c => c.SubscribedToId == subscribedToId).ToList();
            foreach (var t in temp)
            {
                _context.Entry(t).Reference(x => x.Tariff).Load();
            }
            return temp;

        }

        public class UserIdTariffIdSubscibedIdFormat
        {
            public int userId { get; set; }
            public int tariffId { get; set; } 
            public int subscribedToId { get; set; }
        }
        [HttpPost]
        [Route("/PaidSubscriptions/Delete")]
        public async Task Delete([FromBody]UserIdTariffIdSubscibedIdFormat userIdTariffIdSubscibedIdFormat)
        {
            var type = await _context.PaidSubscriptions.FirstAsync(c => c.UserId == userIdTariffIdSubscibedIdFormat.userId && c.Tariff.Id == userIdTariffIdSubscibedIdFormat.tariffId && c.SubscribedToId == userIdTariffIdSubscibedIdFormat.subscribedToId);
            _context.PaidSubscriptions.Remove(type);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/PaidSubscriptions/Add")]
        public async Task Add([FromBody]PaidSubscription newPaidSubscription)
        {
            newPaidSubscription.EndDate = DateTime.Now.AddMonths(1);
            newPaidSubscription.Tariff = _context.Tariffs.First(x => x.Id == newPaidSubscription.TariffId);
            newPaidSubscription.TariffId = newPaidSubscription.Tariff.Id;
            newPaidSubscription.IsAutorenewal = true;
            _context.PaidSubscriptions.Add(newPaidSubscription);
            await _context.SaveChangesAsync();
        }
    }
}
