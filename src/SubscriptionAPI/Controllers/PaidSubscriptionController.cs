﻿using Microsoft.AspNetCore.Mvc;
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
        //=> _context.PaidSubscriptions.ToList();

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
        // => _context.PaidSubscriptions.Where(c => c.UserId == userId).ToList();

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
       //     => _context.PaidSubscriptions.Where(c => c.SubscribedToId == subscribedToId).ToList();

        [HttpPost]
        [Route("/PaidSubscriptions/Delete")]
        public async Task Delete(int userId, int tariffId, int subscribedToId)
        {
            var type = await _context.PaidSubscriptions.FirstAsync(c => c.UserId == userId && c.Tariff.Id == tariffId && c.SubscribedToId == subscribedToId);
            _context.PaidSubscriptions.Remove(type);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/PaidSubscriptions/Add")]
        public async Task Add(int userId, int subscribedToId, int tariffId)
        {
            var newTypeOfSubscription = new PaidSubscription()
            {
                UserId = userId,
                EndDate = DateTime.Now.AddMonths(1),
                SubscribedToId = subscribedToId,
                IsAutorenewal = true,
                Tariff = _context.Tariffs.First(x => x.Id == tariffId)
            };
            _context.PaidSubscriptions.Add(newTypeOfSubscription);
            await _context.SaveChangesAsync();
        }
    }
}
