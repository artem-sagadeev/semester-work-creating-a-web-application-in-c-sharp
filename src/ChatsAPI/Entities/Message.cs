using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatsAPI.Entities
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
