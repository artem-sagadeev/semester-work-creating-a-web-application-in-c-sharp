using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class FileDetails : PageModel
    {
        public string Code { get; set; }
        
        public async Task<IActionResult> OnGet(string name)
        {
            using var reader = new StreamReader($"wwwroot/files/{name}");
            Code = await reader.ReadToEndAsync();
            return Page();
        }
    }
}