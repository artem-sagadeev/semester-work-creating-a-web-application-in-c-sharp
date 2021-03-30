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
            foreach (var t in temp)
            {
                _context.Entry(t).Reference(x => x.TypeOfSubscription).Load();
            }
            return temp;
        }

        [HttpGet]
        [Route("/Tariffs/GetByTariffId/{tariffId}")]
        public ActionResult<Tariff> GetByTariffId(int tariffId)
        {
            var temp = _context.Tariffs.First(c => c.Id == tariffId);
            _context.Entry(temp).Reference(x => x.TypeOfSubscription).Load();
            return temp;
        }
        // => _context.Tariffs.First(c => c.Id == tariffId);

        [HttpGet]
        [Route("/Tariffs/GetBySubscriptionType/{subscriptionTypeId}")]
        public ActionResult<IEnumerable<Tariff>> GetBySubscriptionType(int subscriptionTypeId)
        {
            var temp = _context.Tariffs.Where(c => c.TypeOfSubscription.Id == subscriptionTypeId).ToList();
            foreach (var t in temp)
            {
                _context.Entry(t).Reference(x => x.TypeOfSubscription).Load();
            }
            return temp;
        }
           // => _context.Tariffs.First(c => c.TypeOfSubscription.Id == subscriptionTypeId);

        [HttpPost]
        [Route("/Tariffs/Delete")]
        public async Task Delete(int tariffId)
        {
            var bankAccount = await _context.Tariffs.FirstAsync(c => c.Id == tariffId);
            _context.Tariffs.Remove(bankAccount);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/Tariffs/Add")]
        public async Task AddBankAccount(string name, int pricePerMonth, int typeOfSubscriptionId)
        {
            var newBankAccount = new Tariff()
            {
                TypeOfSubscription = _context.TypeOfSubscriptions.First(x => x.Id == typeOfSubscriptionId),
                Name = name,
                PricePerMonth = pricePerMonth
            };
            _context.Tariffs.Add(newBankAccount);
            await _context.SaveChangesAsync();
        }
    }
}
