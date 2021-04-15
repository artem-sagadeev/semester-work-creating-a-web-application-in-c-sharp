using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controller
{
    public class SubsribeController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET

        private ISubscriptionService _subscriptionService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task Subscribe(int userId, int developerId)
        {
            var newPaidSubscription = new PaidSubscriptionModel()
            {
                SubscribedToId = developerId,
                Tariff = new TariffModel()
                {
                    PriceType = PriceType.Free,
                    TypeOfSubscription = TypeOfSubscription.User
                },
                UserId = userId
            };
            await _subscriptionService.AddPaidSubscription(newPaidSubscription);
        }
    }
}