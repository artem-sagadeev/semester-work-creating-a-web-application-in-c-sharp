using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Controller
{
    public class SubsribeController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        // public Task Subscribe(int userId, int developerId)
        // {
        //     
        // }
    }
}