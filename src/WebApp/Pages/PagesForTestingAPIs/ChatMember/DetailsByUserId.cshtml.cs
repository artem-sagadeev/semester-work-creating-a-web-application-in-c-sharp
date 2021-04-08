using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.ChatMember
{
    public class DetailsByUserIdModel : PageModel
    {
        private readonly IChatService _chatService;
        public IEnumerable<ChatMemberModel> ChatMembers { get; set; }

        public DetailsByUserIdModel(IChatService service)
        {
            _chatService = service;
        }

        public async Task<ActionResult> OnGetAsync(int userId)
        {
            ChatMembers = await _chatService.GetChatMembersByUserId(userId);
            return Page();
        }
    }
}
