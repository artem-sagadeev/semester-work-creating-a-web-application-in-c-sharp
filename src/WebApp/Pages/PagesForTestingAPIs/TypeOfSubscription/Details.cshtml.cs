using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.TypeOfSubscription
{
    public class DetailsModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public TypeOfSubscriptionModel TypesOfSubscrubtionModels { get; set; }

        public DetailsModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            TypesOfSubscrubtionModels = await _subscriptionService.GetTypeOfSubscription(id);
            return Page();
        }
    }
}
