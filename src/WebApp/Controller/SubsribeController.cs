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
        private IPaymentService _paymentService;
        public IActionResult Index()
        {
            return View();
        }

        public async Task Follow(int userId, int developerId)
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

        public async Task Subscribe(int userId, int developerId, bool isBasic, bool isImproved, bool isMax)
        {
            var userAccount = _paymentService.GetBankAccount(userId);
            
            //1. Если впервые, то должен ввести данные карты (BankAccount)
            //2. Берем номер карты, списываем деньги, перекидываем в виртуальный кошелек девелопера, вычитая процент, а все деньги храним в хранилище
            //3. Добавляем в список подписок PaidSubscription

        }
    }
}