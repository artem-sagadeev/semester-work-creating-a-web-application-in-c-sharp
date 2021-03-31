using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Message
{
    public class DetailsByIdModel : PageModel
    {
        private readonly IChatService _chatService;
        public MessageModel MessageModel { get; set; }

        public DetailsByIdModel(IChatService service)
        {
            _chatService = service;
        }

        public async Task<ActionResult> OnGetAsync(int messageId)
        {
            MessageModel = await _chatService.GetMessagesById(messageId);
            return Page();
        }
    }
}
