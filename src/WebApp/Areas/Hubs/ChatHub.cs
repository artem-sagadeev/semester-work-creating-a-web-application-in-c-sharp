using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WebApp.Models.Chats;
using WebApp.Services.Chats;
using WebApp.Services.Developer;

namespace WebApp.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IDeveloperService _developerService;

        public ChatHub(IChatService chatService, IDeveloperService developerService)
        {
            _chatService = chatService;
            _developerService = developerService;
        }

        public async Task Send(string message, int userId, int projectId)
        {
            var messageModel = new MessageModel()
            {
                Text = message,
                UserId = userId,
                DateTime = DateTime.Now,
                ProjectId = projectId
            };
            
            //TODO: Раскомментирвоать
            await _chatService.AddMessage(messageModel);
            var  userName = (await _developerService.GetUser(userId)).Name;
            //var userName = userId.ToString();
            await Clients.All.SendAsync("Send", message, userName);
        }
    }
}