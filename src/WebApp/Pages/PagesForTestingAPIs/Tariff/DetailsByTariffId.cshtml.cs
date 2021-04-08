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
    public class DetailsByTariffIdModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public TariffModel TariffModels { get; set; }

        public DetailsByTariffIdModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync(int tariffId)
        {
            TariffModels = await _subscriptionService.GetTariffById(tariffId);
            return Page();
        }
    }
}
