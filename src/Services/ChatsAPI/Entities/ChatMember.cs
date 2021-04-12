using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsAPI.Entities
{
    public class ChatMember
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public bool IsAuthor { get; set; }
    }
}