using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApp.Models;
using WebApp.Models.Chats;
using WebApp.Models.Developer;
using WebApp.Models.Payment;
using WebApp.Models.Subscription;
using WebApp.Services;
using WebApp.Services.Chats;
using WebApp.Services.Developer;
using WebApp.Services.Payment;
using WebApp.Services.Subscription;

namespace WebApp
{
    public class SubscribeHandler
    {
        // GET

        private readonly ISubscriptionService _subscriptionService;
        private readonly IPaymentService _paymentService;
        private readonly IDeveloperService _developerService;
        private readonly IChatService _chatService;
        private readonly IServiceProvider _serviceProvider;

        public SubscribeHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _subscriptionService = serviceProvider.GetRequiredService<ISubscriptionService>();
            _developerService = serviceProvider.GetRequiredService<IDeveloperService>();
            _paymentService = serviceProvider.GetRequiredService<IPaymentService>();
            _chatService = serviceProvider.GetRequiredService<IChatService>();

        }

        public bool HasBankAccount(int userId)
        {
            return _paymentService.GetBankAccount(userId) != null;
        }

        public async Task Follow(int userId, int subscribedToId, TypeOfSubscription typeOfSubscription)
        {
            var newPaidSubscription = new PaidSubscriptionModel()
            {
                SubscribedToId = subscribedToId,
                Tariff = new TariffModel()
                {
                    PriceType = PriceType.Free,
                    TypeOfSubscription = typeOfSubscription
                },
                UserId = userId
            };
            await _subscriptionService.AddPaidSubscription(newPaidSubscription);
        }

        //1. Если впервые, то должен ввести данные карты (BankAccount)
        //2. Берем номер карты, списываем деньги, переводя их в хранилище, фиксируем списывание
        //3. Перекидываем в виртуальный кошелек девелопера, вычитая процент
        //4. Добавляем в список подписок PaidSubscription
        //5. Добавляет в список членов чата (если тариф позволяет)
        public async Task Subscribe(int userId, int subscribedToId, bool isBasic, bool isImproved, bool isMax,
          TypeOfSubscription typeOfSubscription)
        {
            //2. ---Берем номер карты, списываем деньги, переводя их в хранилище, фиксируем списывание
            var priceType =
                await WriteOffMoneyFromUserAndGetPriceType(userId, typeOfSubscription, isBasic, isImproved, isMax);
            var tariff =
                await _subscriptionService.GetTariffByPriceTypeAndSubscriptionType(typeOfSubscription, priceType);
            if (typeOfSubscription == TypeOfSubscription.User)
            {
                //Перекидываем в виртуальный кошелек девелопера, вычитая процент
                var developerPurse = await _paymentService.GetVirtualPurse(subscribedToId);
                var adminMoney = (int)(tariff.PricePerMonth * PercentForAdmin);
                await _paymentService.UpdateVirtualPurse(subscribedToId,
                    developerPurse.Money + tariff.PricePerMonth - adminMoney);
                await _paymentService.TransferMoneyToAdminPurse(adminMoney);
            }
            else
            {
                // Перекидываем в виртуальный кошелек девелоперОВ, вычитая процент
                IEnumerable<UserModel> allDevelopers = new List<UserModel>();
                switch (typeOfSubscription)
                {
                    case TypeOfSubscription.Project:
                        allDevelopers = await _developerService.GetProjectUsers(subscribedToId);
                        break;
                    case TypeOfSubscription.Team:
                        allDevelopers = await _developerService.GetCompanyUsers(subscribedToId);
                        break;
                }
                await SendMoneyToVirtualPursesOfDevs(tariff, allDevelopers);
            }
            //Добавляем в список подписок 
            await _subscriptionService.AddPaidSubscription(new PaidSubscriptionModel()
            {
                SubscribedToId = subscribedToId,
                Tariff = new TariffModel()
                {
                    PriceType = priceType,
                    TypeOfSubscription = typeOfSubscription
                },
                UserId = userId
            });
            //Добавляем в чаты
            if (typeOfSubscription == TypeOfSubscription.Project)
            {
                var currentChatMember = new ChatMemberModel()
                {
                    IsAuthor = false,
                    ProjectId = subscribedToId,
                    UserId = userId
                };
                await _chatService.AddChatMember(currentChatMember);
            }
            else
            {
                IEnumerable<ProjectModel> projects = new List<ProjectModel>();
                switch (typeOfSubscription)
                {
                    case TypeOfSubscription.User:
                        projects = await _developerService.GetUserProjects(subscribedToId);
                        break;
                    case TypeOfSubscription.Team:
                        projects = await _developerService.GetCompanyProjects(subscribedToId);
                        break;
                }

                if (projects != null)
                {
                    foreach (var project in projects)
                    {
                        var currentChatMember = new ChatMemberModel()
                        {
                            IsAuthor = false,
                            ProjectId = project.Id,
                            UserId = userId
                        };
                        await _chatService.AddChatMember(currentChatMember);
                    }
                }
            }
        }


        //TODO: процент где фиксировать?
        public double PercentForAdmin = 0.1;

        private async Task SendMoneyToVirtualPursesOfDevs(TariffModel tariff, IEnumerable<UserModel> allDevelopers)
        {
            //Перевидываем процент админу, остальное распределяем по разработчикам
            var adminMoney = (int)(tariff.PricePerMonth * PercentForAdmin);
            await _paymentService.TransferMoneyToAdminPurse(adminMoney);
            if (allDevelopers != null)
            {
                foreach (var developer in allDevelopers)
                {
                    var developerPurse = await _paymentService.GetVirtualPurse(developer.Id); //  
                    await _paymentService.UpdateVirtualPurse(developer.Id,
                        developerPurse.Money + (tariff.PricePerMonth - adminMoney) / allDevelopers.Count());
                }
            }
        }

        private async Task<PriceType> WriteOffMoneyFromUserAndGetPriceType(int userId,
            TypeOfSubscription typeOfSubscription, bool isBasic, bool isImproved, bool isMax)
        {
            //Берем номер карты, списываем деньги, переводя их в хранилище, фиксируем списывание
            var userAccount = await _paymentService.GetBankAccount(userId);
            var priceType = PriceType.Max;
            if (isBasic)
                priceType = PriceType.Basic;
            if (isImproved)
                priceType = PriceType.Improved;
            if (isMax)
                priceType = PriceType.Max;
            var tariff =
                await _subscriptionService.GetTariffByPriceTypeAndSubscriptionType(typeOfSubscription, priceType);
            await _paymentService.WriteOffMoneyFromBankAccount(userAccount, tariff.PricePerMonth);
            await _paymentService.AddWithdrawal(new WithdrawalModel()
            {
                Sum = tariff.PricePerMonth,
                DateTime = DateTime.Now,
                UserID = userId,
                ViewOfBankNumber = ViewOfBankNumber.Real
            });
            await _paymentService.AddMoneyToStorageOfMoney(tariff.PricePerMonth);
            return priceType;
        }
    }
}