using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubscriptionAPI.Entities;

namespace SubscriptionAPI.Controllers
{
    public class TariffController : Controller
    {
        private readonly SubscriptionContext _context;
        public TariffController(SubscriptionContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/Tariffs/Get")]
        public ActionResult<IEnumerable<Tariff>> Get()
        {
            var temp = _context.Tariffs.ToList();
            // foreach (var t in temp)
            // {
            //     _context.Entry(t).Reference(x => x.TypeOfSubscription).Load();
            // }
            return temp;
        }

        [HttpGet]
        [Route("/Tariffs/GetByTariffId/{tariffId}")]
        public ActionResult<Tariff> GetByTariffId(int tariffId)
        {
            var temp = _context.Tariffs.First(c => c.Id == tariffId);
            //_context.Entry(temp).Reference(x => x.TypeOfSubscription).Load();
            return temp;
        }
        // => _context.Tariffs.First(c => c.Id == tariffId);
        
        

        [HttpGet]
        [Route("/Tariffs/GetBySubscriptionTypeAndPiceType/{subscriptionTypeId}/{priceType}")]
        public ActionResult<Tariff> GetBySubscriptionTypeAndPiceType(TypeOfSubscription subscriptionTypeId, PriceType priceType)
        {
            var all = _context.Tariffs.ToList();
            var temp = _context.Tariffs.First(c => c.TypeOfSubscription == subscriptionTypeId && c.PriceType == priceType);
            return temp;
        }
        
        [HttpGet]
        [Route("/Tariffs/GetBySubscriptionType/{subscriptionTypeId}")]
        public ActionResult<IEnumerable<Tariff>> GetBySubscriptionType(TypeOfSubscription subscriptionTypeId)
        {
            var temp = _context.Tariffs.Where(c => c.TypeOfSubscription == subscriptionTypeId).ToList();
            // foreach (var t in temp)
            // {
            //     _context.Entry(t).Reference(x => x.TypeOfSubscription).Load();
            // }
            return temp;
        }
        
        public class TariffIdFormat
        {
            public int tariffId { get; set; }
        }

        [HttpPost]
        [Route("/Tariffs/Delete")]
        public async Task Delete([FromBody]TariffIdFormat tariffIdFormat)
        {
            var bankAccount = await _context.Tariffs.FirstAsync(c => c.Id == tariffIdFormat.tariffId);
            _context.Tariffs.Remove(bankAccount);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Tariffs/Add")]
        public async Task Add([FromBody]Tariff newTariff)
        {
            //newTariff.TypeOfSubscription = _context.TypeOfSubscriptions.First(x => x.Id == newTariff.TypeOfSubscription.Id);
            _context.Tariffs.Add(newTariff);
            await _context.SaveChangesAsync();
        }
    }
}
