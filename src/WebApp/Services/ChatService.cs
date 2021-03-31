using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public class ChatService :IChatService
    {
        private readonly HttpClient _client;

        public ChatService(HttpClient client)
        {
            _client = client;
        }

        //Chats

        public async Task<IEnumerable<ChatMemberModel>> GetChatMembers()
        {
            var response = await _client.GetAsync($"/Chats/GetChatMembers");
            return await response.ReadContentAs<IEnumerable<ChatMemberModel>>();
        }

        public async Task<IEnumerable<ChatMemberModel>> GetChatMembersByProjectId(int projectId)
        {
            var response = await _client.GetAsync($"/Chats/GetChatMembersByProjectId?projectId={projectId}");
            return await response.ReadContentAs<IEnumerable<ChatMemberModel>>();
        }

        public async Task<IEnumerable<ChatMemberModel>> GetChatMembersByUserId(int userId)
        {
            var response = await _client.GetAsync($"/Chats/GetChatMembersByUserId?userId={userId}");
            return await response.ReadContentAs<IEnumerable<ChatMemberModel>>();
        }
        
        //Messages

        public async Task<IEnumerable<MessageModel>> GetMessages()
        {
            var response = await _client.GetAsync($"/Chats/GetMessages");
            return await response.ReadContentAs<IEnumerable<MessageModel>>();
        }

        public async Task<IEnumerable<MessageModel>> GetMessagesById(int messageId)
        {
            var response = await _client.GetAsync($"/Chats/GetMessagesById?messageId={messageId}");
            return await response.ReadContentAs<IEnumerable<MessageModel>>();
        }

        public async Task<IEnumerable<MessageModel>> GetMessagesByProjectId(int projectId)
        {
            var response = await _client.GetAsync($"/Chats/GetMessagesByProjectId?projectId={projectId}");
            return await response.ReadContentAs<IEnumerable<MessageModel>>();
        }

        public async Task<IEnumerable<MessageModel>> GetMessagesByUserIdAndProjectId(int userId, int projectId)
        {
            var response = await _client.GetAsync($"/Chats/GetMessagesByUserIdAndProjectId?projectId={projectId}&userId={userId}");
            return await response.ReadContentAs<IEnumerable<MessageModel>>();
        }
    }
}
