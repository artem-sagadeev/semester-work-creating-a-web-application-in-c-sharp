using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionAPI.Controllers
{
    public class PaidSubscriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
