using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatsAPI.Controllers
{
    public class ChatMemberController : Controller
    {
        private readonly ChatsContext _context;
        public ChatMemberController(ChatsContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/ChatsMembers/Get")]
        public ActionResult<IEnumerable<ChatMember>> Get() => _context.ChatsMembers.ToList();

        [HttpGet]
        [Route("/ChatsMembers/GetByProjectId/{projectId}")]
        public ActionResult<IEnumerable<ChatMember>> GetByProjectId(int projectId) =>
            _context.ChatsMembers.Where(c => c.ProjectId == projectId).ToList();


        [HttpGet]
        [Route("/ChatsMembers/GetByUserId/{userId}")]
        public ActionResult<IEnumerable<ChatMember>> GetByUserId(int userId) =>
            _context.ChatsMembers.Where(c => c.UserId == userId).ToList();

        public class ProjectUserIdFormat
        {
            public int projectId { get; set; }

            public int userId { get; set; }
        }
        
        [HttpPost]
        [Route("/ChatsMembers/Delete")]
        public async Task Delete([FromBody]ProjectUserIdFormat projectUserIdFormat)
        {
            var member = await _context.ChatsMembers.FirstAsync(c => c.UserId == projectUserIdFormat.userId && c.ProjectId == projectUserIdFormat.projectId);
            _context.ChatsMembers.Remove(member);
            await _context.SaveChangesAsync();
        }

        [HttpPost]
        [Route("/ChatsMembers/Add")]
        public async Task Add([FromBody]ChatMember newChatMember)
        {
            var chatMember = new ChatMember()
            {
               ProjectId = newChatMember.ProjectId,
               UserId = newChatMember.UserId,
               IsAuthor = newChatMember.IsAuthor
            };
            _context.ChatsMembers.Add(chatMember);
            await _context.SaveChangesAsync();
        }


    }
}
