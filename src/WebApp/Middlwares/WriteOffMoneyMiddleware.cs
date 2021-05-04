using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services.Payment;
using WebApp.Services.Subscription;

namespace WebApp.Middlwares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class WriteOffMoneyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IPaymentService _paymentService;

        public WriteOffMoneyMiddleware(RequestDelegate next, ISubscriptionService subscriptionService, IPaymentService paymentService)
        {
            _next = next;
            _subscriptionService = subscriptionService;
            _paymentService = paymentService;
        }

        public Task Invoke(HttpContext httpContext)
        {
            // //TODO: проверка на авторизованность
            // //Взятие id пользователя
            // int userId =0;
            // var allPaidSubscriptions = await _subscriptionService.GetPaidSubscriptionsByUserId(userId);
            // foreach (var paidSubscription in allPaidSubscriptions)  
            // {
            //     if (paidSubscription.EndDate == DateTime.Today)
            //     {
            //         _paymentService.WriteOffMoneyFromBankAccount(BankAccountModel newBankAccount, int money);
            //
            //     }
            // }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class WriteOffMoneyMiddlewareExtensions
    {
        public static IApplicationBuilder UseWriteOffMoneyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<WriteOffMoneyMiddleware>();
        }
    }
}
