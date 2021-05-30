using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Models.Identity;
using WebApp.Models.Payment;
using WebApp.Models.Subscription;
using WebApp.Services.Developer;
using WebApp.Services.Payment;
using WebApp.Services.Subscription;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Middlwares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class WriteOffMoneyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IPaymentService _paymentService;
        private readonly IDeveloperService _developerService;

        public WriteOffMoneyMiddleware(RequestDelegate next, ISubscriptionService subscriptionService, 
            IPaymentService paymentService, IDeveloperService developerService)
        {
            _next = next;
            _subscriptionService = subscriptionService;
            _paymentService = paymentService;
            _developerService = developerService;
        }

        public async Task Invoke(HttpContext httpContext, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager)
        {
            if (_signInManager.IsSignedIn(httpContext.User))
            {
                var userId = (await _userManager.GetUserAsync(httpContext.User)).UserId;
                var allPaidSubscriptions = await _subscriptionService.GetPaidSubscriptionsByUserId(userId);
                var userBankAccount = await _paymentService.GetBankAccount(userId);
                foreach (var paidSubscription in allPaidSubscriptions)
                {
                    if (paidSubscription.EndDate == DateTime.Today)
                    {
                        //Write off money of user
                        await _paymentService.WriteOffMoneyFromBankAccount(userBankAccount,
                            paidSubscription.Tariff.PricePerMonth);
                        await _paymentService.AddMoneyToStorageOfMoney(paidSubscription.Tariff.PricePerMonth);
                        await _paymentService.AddWithdrawal(new WithdrawalModel()
                        {
                            Sum = paidSubscription.Tariff.PricePerMonth,
                            DateTime = DateTime.Now,
                            UserID = userId,
                            ViewOfBankNumber = ViewOfBankNumber.Real
                        });
                        //Fill up developer's purse
                        var typeOfSubscription = paidSubscription.Tariff.TypeOfSubscription;
                        switch (typeOfSubscription)
                        {
                            case TypeOfSubscription.Project:
                                FillUpProjectPurse(paidSubscription.SubscribedToId,
                                    paidSubscription.Tariff.PricePerMonth);
                                break;
                            case TypeOfSubscription.Team:
                                FillUpTeamPurse(paidSubscription.SubscribedToId, paidSubscription.Tariff.PricePerMonth);
                                break;
                            case TypeOfSubscription.User:
                                FillUpUserPurse(paidSubscription.SubscribedToId, paidSubscription.Tariff.PricePerMonth);
                                break;
                        }
                    }
                }
            }

            await _next(httpContext);
        }

        public double PercentForAdmin = 0.1;

        public async void FillUpUserPurse(int developerId, int price)
        {
            //Пополняем кошелек админа и разраба
            var adminMoney = (int) (price * PercentForAdmin);
            await _paymentService.TransferMoneyToAdminPurse(adminMoney);
            var developerPurse = await _paymentService.GetVirtualPurse(developerId);
            await _paymentService.UpdateVirtualPurse(developerId,
                developerPurse.Money + (price - adminMoney));
        }

        public async void FillUpTeamPurse(int companyId, int pricePerMonth)
        {
            var allProjects = await _developerService.GetCompanyProjects(companyId);
            var pricePerProject = pricePerMonth / allProjects.Count();
            foreach (var project in allProjects)
            { 
                FillUpProjectPurse(project.Id, pricePerProject);
            }
        }

        public async void FillUpProjectPurse(int projectId, int pricePerMonth)
        {
            //Переводит деньги админу и каждому разрабу
            var developers = await _developerService.GetProjectUsers(projectId);
            var adminMoney = (int) (pricePerMonth * PercentForAdmin);
            await _paymentService.TransferMoneyToAdminPurse(adminMoney);
            foreach (var developer in developers)
            {
                var developerId = developer.Id;
                var developerPurse = await _paymentService.GetVirtualPurse(developerId);
                await _paymentService.UpdateVirtualPurse(developer.Id,
                    developerPurse.Money + (pricePerMonth - adminMoney) / developers.Count());
            }
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