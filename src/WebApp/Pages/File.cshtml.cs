using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages
{
    public class File : PageModel
    {
        private readonly IFileService _fileService;

        public File(IFileService fileService)
        {
            _fileService = fileService;
        }

        public string Uri { get; set; }
        
        public async Task<ActionResult> OnGetAsync(string id)
        {
            Uri = await _fileService.GetLink(id);
            return Page();
        }
    }
}