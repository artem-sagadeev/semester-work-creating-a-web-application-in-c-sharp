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
    public class DetailsByProjectIdModel : PageModel
    {
        private readonly IChatService _chatService;
        public IEnumerable<MessageModel> MessageModels { get; set; }

        public DetailsByProjectIdModel(IChatService service)
        {
            _chatService = service;
        }

        public async Task<ActionResult> OnGetAsync(int projectId)
        {
            MessageModels = await _chatService.GetMessagesByProjectId(projectId);
            return Page();
        }
    }
}
