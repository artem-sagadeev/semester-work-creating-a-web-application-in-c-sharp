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
            => _context.Tariffs.ToList();

        [HttpGet]
        [Route("/Tariffs/GetByTariffId/{tariffId}")]
        public ActionResult<Tariff> GetByTariffId(int tariffId)
            => _context.Tariffs.First(c => c.Id == tariffId);

        [HttpGet]
        [Route("/Tariffs/GetBySubscriptionType/{subscriptionTypeId}")]
        public ActionResult<Tariff> GetBySubscriptionType(int subscriptionTypeId)
            => _context.Tariffs.First(c => c.Type.Id == subscriptionTypeId);

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
        public async Task AddBankAccount(string name,  int pricePerMonth, int typeOfSubscriptionId)
        {
            var newBankAccount = new Tariff()
            {
                Type = _context.TypeOfSubscriptions.First(x=> x.Id == typeOfSubscriptionId),
                Name = name,
                PricePerMonth = pricePerMonth
            };
            _context.Tariffs.Add(newBankAccount);
            await _context.SaveChangesAsync();
        }
    }
}
