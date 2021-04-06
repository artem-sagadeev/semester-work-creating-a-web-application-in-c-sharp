using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ChatMemberModel
    {
        public int ProjectId { get; set; }

        public int UserId { get; set; }

        public bool IsAuthor { get; set; }
    }
}
