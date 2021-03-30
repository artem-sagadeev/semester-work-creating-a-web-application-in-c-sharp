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
    public class IndexModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;
        public IEnumerable<TypeOfSubscriptionModel> TypesOfSubscrubtionModels { get; set; }

        public IndexModel(ISubscriptionService service)
        {
            _subscriptionService = service;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            TypesOfSubscrubtionModels = await _subscriptionService.GetTypesOfSubscription();
            return Page();
        }
    }
}
