﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<IEnumerable<TariffModel>> GetTariffBySubscriptionType(TypeOfSubscription subscriptionTypeId)
        {
            var response = await _client.GetAsync($"/Subscription/GetTariffBySubscriptionType?subscriptionTypeId={subscriptionTypeId}");
            return await response.ReadContentAs<IEnumerable<TariffModel>>();
        }

        public async Task AddTariff(TariffModel newTariff)
        {
            await _client.PostAsJsonAsync($"/Subscription/AddTariff", newTariff);
        }

        private class TariffIdFormat
        {
            public int tariffId { get; set; }
        }
        public async Task DeleteTariff(int tariffId)
        {
            await _client.PostAsJsonAsync($"/Subscription/DeleteTariff", new TariffIdFormat()
            {
                tariffId = tariffId
            });
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

        public async Task AddPaidSubscription(PaidSubscriptionModel newPaidSubscription)
        {
            await _client.PostAsJsonAsync($"/Subscription/AddPaidSubscription", newPaidSubscription);
        }

        class UserIdTariffIdSubscibedIdFormat
        {
            public int userId { get; set; }
            public int tariffId { get; set; } 
            public int subscribedToId { get; set; }
        }
        
        public async Task DeletePaidSubscription(int userId, int tariffId, int subscribedToId)
        {
            await _client.PostAsJsonAsync($"/Subscription/DeletePaidSubscription", 
                new UserIdTariffIdSubscibedIdFormat()
                {
                    userId = userId,
                    tariffId = tariffId,
                    subscribedToId = subscribedToId
                });
        }
    }
}