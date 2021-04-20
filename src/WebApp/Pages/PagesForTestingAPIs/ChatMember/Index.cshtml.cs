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
    public class IndexModel : PageModel
    {
        private readonly IChatService _chatService;
        public IEnumerable<ChatMemberModel> ChatMembers { get; set; }

        public IndexModel(IChatService service)
        {
            _chatService = service;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            ChatMembers = await _chatService.GetChatMembers();
            return Page();
        }
        public async Task OnPostAsync(int userId, int projectId)
        {
            await _chatService.DeleteChatMember(projectId, userId);
        }
    }
}
