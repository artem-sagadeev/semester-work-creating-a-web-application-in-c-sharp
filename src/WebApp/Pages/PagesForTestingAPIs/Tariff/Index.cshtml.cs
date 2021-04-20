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

namespace WebApp.Pages.Tariff
{
    public class IndexModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public IEnumerable<TariffModel> TariffModels { get; set; }

        public IndexModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            TariffModels = await _subscriptionService.GetTariffs();
            return Page();
        }

        public async Task OnPostAsync(int tariffId)
        {
            var x = new TariffModel()
            {
                PriceType = PriceType.Free,
                TypeOfSubscription = TypeOfSubscription.Team,
                PricePerMonth = 10
            };
            await _subscriptionService.AddTariff(x);
        }
    }
}

