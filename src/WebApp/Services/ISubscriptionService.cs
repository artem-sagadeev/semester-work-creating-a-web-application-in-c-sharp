using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface ISubscriptionService
    {
        //Tariffs
        public Task<IEnumerable<TariffModel>> GetTariffs();
        public Task<TariffModel> GetTariffById(int tariffId);
        public Task<IEnumerable<TariffModel>> GetTariffBySubscriptionType(int subscriptionTypeId);

       
        //TypeOfSubscriptions
        public Task<IEnumerable<TypeOfSubscriptionModel>> GetTypesOfSubscription();
        public Task<TypeOfSubscriptionModel> GetTypeOfSubscription(int id);

        //PaidSubscription
        public Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptions();
        public Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptionsByUserId(int userId);
        public Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptionsBySubscribedToId(int subscribedToId);

    }
}
