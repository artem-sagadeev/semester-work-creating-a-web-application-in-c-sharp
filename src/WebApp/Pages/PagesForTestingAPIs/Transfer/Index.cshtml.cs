using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Transfer
{
    public class IndexModel : PageModel
    {
        private readonly IPaymentService _paymentService;
        public IEnumerable<TransferModel> TransferModels { get; set; }

        public IndexModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            TransferModels = await _paymentService.GetTransfers();
            return Page();
        }

        public async Task OnPostAsync(int money, int userFrom, int userTo)
        {
            var x = new TransferModel()
            {
                MoneySum = money,
                UserFrom = userFrom,
                UserTo = userTo,
            };
            await _paymentService.AddTransfer(x);
        }
    }
}
