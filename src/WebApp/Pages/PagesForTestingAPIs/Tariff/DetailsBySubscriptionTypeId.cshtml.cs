using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Tariff
{
    public class DetailsBySubscriptionTypeIdModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public IEnumerable<TariffModel> TariffModels { get; set; }

        public DetailsBySubscriptionTypeIdModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync(int subscriptionTypeId)
        {
            TariffModels = await _subscriptionService.GetTariffBySubscriptionType(subscriptionTypeId);
            return Page();
        }
    }
}
