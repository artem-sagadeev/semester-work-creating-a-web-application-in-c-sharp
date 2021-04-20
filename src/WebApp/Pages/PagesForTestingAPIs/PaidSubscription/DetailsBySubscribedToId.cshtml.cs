using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Subscription;
using WebApp.Services;
using WebApp.Services.Subscription;

namespace WebApp.Pages.PaidSubscription
{
    public class DetailsBySubscribedToIdModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public IEnumerable<PaidSubscriptionModel> PaidSubscriptionModels { get; set; }

        public DetailsBySubscribedToIdModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync(int subscribedToId)
        {
            PaidSubscriptionModels = await _subscriptionService.GetPaidSubscriptionsBySubscribedToId(subscribedToId);
            return Page();
        }
    }
}
