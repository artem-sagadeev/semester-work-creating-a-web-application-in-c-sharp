using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models.Chats;

namespace WebApp.Services.Chats
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

        private class ProjectUserIdFormat
        {
            public int projectId { get; set; }

            public int userId { get; set; }
        }
        public async Task DeleteChatMember(int projectId, int userId)
        {
            await _client.PostAsJsonAsync($"/Chats/DeleteChatMember", new ProjectUserIdFormat()
            {
                projectId = projectId,
                userId = userId
            });
        }

        public async Task AddChatMember(ChatMemberModel chatMember)
        {
            await _client.PostAsJsonAsync($"/Chats/AddChatMember", chatMember);
        }

        //Messages

        public async Task<IEnumerable<MessageModel>> GetMessages()
        {
            var response = await _client.GetAsync($"/Chats/GetMessages");
            return await response.ReadContentAs<IEnumerable<MessageModel>>();
        }

        public async Task<MessageModel> GetMessagesById(int messageId)
        {
            var response = await _client.GetAsync($"/Chats/GetMessagesById?messageId={messageId}");
            return await response.ReadContentAs<MessageModel>();
        }

        public async Task<IEnumerable<MessageModel>> GetMessagesByProjectId(int projectId)
        {
            var response = await _client.GetAsync($"/Chats/GetMessagesByProjectId?projectId={projectId}");
            return await response.ReadContentAs<IEnumerable<MessageModel>>();
        }

        public async Task<IEnumerable<MessageModel>> GetMessagesByUserIdAndProjectId(int userId, int projectId)
        {
            var response = await _client.GetAsync($"/Chats/GetMessagesByUserIdAndProjectId?userId={userId}&projectId={projectId}");
            return await response.ReadContentAs<IEnumerable<MessageModel>>();
        }

        public async Task AddMessage(MessageModel message)
        {
           await _client.PostAsJsonAsync($"/Chats/AddMessage", message);
        }

        private class MessageIdFormat
        {
            public int messageId { get; set; }
        }
        public async Task DeleteMessage(int messageId)
        {
            await _client.PostAsJsonAsync($"/Chats/DeleteMessage", new MessageIdFormat()
            {
                messageId = messageId
            });
        }
    }
}
