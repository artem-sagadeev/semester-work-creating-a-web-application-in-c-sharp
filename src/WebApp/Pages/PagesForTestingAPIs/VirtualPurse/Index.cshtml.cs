using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.VirtualPurse
{
    public class IndexModel : PageModel
    {
        private readonly IPaymentService _paymentService;
        public IEnumerable<VirtualPurseModel> VirtualPursesModels { get; set; }

        public IndexModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            VirtualPursesModels = await _paymentService.GetVirtualPurses();
            return Page();
        }
    }
}
