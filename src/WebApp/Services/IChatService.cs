using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IChatService
    {
        //ChatMembers
        public Task<IEnumerable<ChatMemberModel>> GetChatMembers();
        public Task<IEnumerable<ChatMemberModel>> GetChatMembersByProjectId(int projectId);

        public Task<IEnumerable<ChatMemberModel>> GetChatMembersByUserId(int userId);

        //Messages
        public Task<IEnumerable<MessageModel>> GetMessages();
        public Task<MessageModel> GetMessagesById(int messageId);

        public Task<IEnumerable<MessageModel>> GetMessagesByProjectId(int projectId);
        public Task<IEnumerable<MessageModel>> GetMessagesByUserIdAndProjectId(int userId, int projectId);
      

        

    }
}
