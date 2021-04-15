using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly HttpClient _client;

        public SubscriptionService(HttpClient client)
        {
            _client = client;
        }

        //Tariffs
        public async Task<IEnumerable<TariffModel>> GetTariffs()
        {
            var response = await _client.GetAsync($"/Subscription/GetTariffs");
            return await response.ReadContentAs<IEnumerable<TariffModel>>();
        }

        public async Task<TariffModel> GetTariffById(int tariffId)
        {
            var response = await _client.GetAsync($"/Subscription/GetTariffById?tariffId={tariffId}");
            return await response.ReadContentAs<TariffModel>();
        }

        public async Task<IEnumerable<TariffModel>> GetTariffBySubscriptionType(int subscriptionTypeId)
        {
            var response = await _client.GetAsync($"/Subscription/GetTariffBySubscriptionType?subscriptionTypeId={subscriptionTypeId}");
            return await response.ReadContentAs<IEnumerable<TariffModel>>();
        }

        //TypeOfSubscription
        public async Task<IEnumerable<TypeOfSubscriptionModel>> GetTypesOfSubscription()
        {
            var response = await _client.GetAsync($"/Subscription/GetTypesOfSubscription");
            return await response.ReadContentAs<IEnumerable<TypeOfSubscriptionModel>>();
        }

        public async Task<TypeOfSubscriptionModel> GetTypeOfSubscription(int id)
        {
            var response = await _client.GetAsync($"/Subscription/GetTypeOfSubscription?id={id}");
            return await response.ReadContentAs<TypeOfSubscriptionModel>();
        }

        //PaidSubscriptions
        public async Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptions()
        {
            var response = await _client.GetAsync($"/Subscription/GetPaidSubscriptions");
            return await response.ReadContentAs<IEnumerable<PaidSubscriptionModel>>();
        }

        public async Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptionsByUserId(int userId)
        {
            var response = await _client.GetAsync($"/Subscription/GetPaidSubscriptionsByUserId?userId={userId}");
            return await response.ReadContentAs<IEnumerable<PaidSubscriptionModel>>();
        }

        public async Task<IEnumerable<PaidSubscriptionModel>> GetPaidSubscriptionsBySubscribedToId(int subscribedToId)
        {
            var response = await _client.GetAsync($"/Subscription/GetPaidSubscriptionsBySubscribedToId?subscribedToId={subscribedToId}");
            return await response.ReadContentAs<IEnumerable<PaidSubscriptionModel>>();
        }

        public Task<bool> HasUserAccess(int userId, int subscribedToId, int subscriptionType)
        {
            throw new NotImplementedException();
        }
    }
}
