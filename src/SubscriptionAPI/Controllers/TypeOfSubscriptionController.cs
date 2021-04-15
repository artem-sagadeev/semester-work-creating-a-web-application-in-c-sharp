using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubscriptionAPI.Entities;

namespace SubscriptionAPI.Controllers
{
    public class TypeOfSubscriptionController : Controller
    {
        private readonly SubscriptionContext _context;
        public TypeOfSubscriptionController(SubscriptionContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/TypeOfSubscriptions/Get")]
        public ActionResult<IEnumerable<TypeOfSubscription>> Get()
            => _context.TypeOfSubscriptions.ToList();

        [HttpGet]
        [Route("/TypeOfSubscriptions/Get/{id}")]
        public ActionResult<TypeOfSubscription> Get(int id)
            => _context.TypeOfSubscriptions.First(c => c.Id == id);

        public class IdFormat
        {
            public int id { get; set; }
        }
        [HttpPost]
        [Route("/TypeOfSubscriptions/Delete")]
        public async Task Delete([FromBody]IdFormat id)
        {
            var type = await _context.TypeOfSubscriptions.FirstAsync(c => c.Id == id.id);
            _context.TypeOfSubscriptions.Remove(type);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/TypeOfSubscriptions/Add")]
        public async Task Add([FromBody]TypeOfSubscription newTypeOfSubscription)
        {
           _context.TypeOfSubscriptions.Add(newTypeOfSubscription);
            await _context.SaveChangesAsync();
        }
    }
}
