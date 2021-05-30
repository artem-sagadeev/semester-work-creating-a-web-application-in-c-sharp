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
    public class IndexModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public IEnumerable<PaidSubscriptionModel> PaidSubscriptionModels { get; set; }

        public IndexModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            var x = await _subscriptionService.HasUserAccess(1, 3, PriceType.Free, TypeOfSubscription.User);
            return null;
        }

        // public async Task OnPostAsync(int userId, int subscribedToId, int tariffId)
        // {
        //     await _subscriptionService.DeletePaidSubscription(userId, tariffId, subscribedToId);
        // }
    }
}
