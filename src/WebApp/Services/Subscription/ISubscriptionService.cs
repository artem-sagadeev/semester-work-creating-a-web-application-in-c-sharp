using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models.Subscription;

namespace WebApp.Services.Subscription
{
    public interface ISubscriptionService
    {
        //Tariffs
        public Task<IEnumerable<TariffModel>> GetTariffs();
        public Task<TariffModel> GetTariffById(int tariffId);
        public Task<IEnumerable<TariffModel>> GetTariffBySubscriptionType(TypeOfSubscription subscriptionTypeId);

        public Task AddTariff(TariffModel newTariff);

        public Task DeleteTariff(int tariffId);

        public Task<TariffModel> GetTariffByPriceTypeAndSubscriptionType(TypeOfSubscription subscriptionTypeId,
            PriceType priceType);
        
        //PaidSubscription
        public Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptions();
        public Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptionsByUserId(int userId);
        public Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptionsBySubscribedToId(int subscribedToId);

        public Task AddPaidSubscription(PaidSubscriptionModel newPaidSubscription);

        public Task DeletePaidSubscription(int userId, int tariffId, int subscribedToId);
        
        //Other
        public Task<bool> HasUserAccess(int subscriberId, int subscribedToId, PriceType type);
    }
}
