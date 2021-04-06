using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.PaidSubscription
{
    public class DetailsByUserIdModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public IEnumerable<PaidSubscriptionModel> PaidSubscriptionModels { get; set; }

        public DetailsByUserIdModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync(int userId)
        {
            PaidSubscriptionModels = await _subscriptionService.GetPaidSubscriptionsByUserId(userId);
            return Page();
        }
    }
}
