using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Pages.Creators
{
    public class Index : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Index(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        
        public List<ICreator> Creators { get; set; }

        public async Task<ActionResult> OnGetAsync(string needUsers, 
            string needProjects, 
            string needCompanies, 
            string searchString,
            string sortOption)
        {
            if (needUsers == "on")
                Creators.AddRange(sortOption == null ? 
                    await _developerService.GetUsers() : 
                    await _developerService.GetUsersByName(searchString));
            
            if (needProjects == "on")
                Creators.AddRange(sortOption == null ? 
                    await _developerService.GetProjects() : 
                    await _developerService.GetProjectsByName(searchString));
            
            if (needCompanies == "on")
                Creators.AddRange(sortOption == null ? 
                    await _developerService.GetCompanies() : 
                    await _developerService.GetCompaniesByName(searchString));
            
            if (sortOption != null)
                Sort(sortOption);
            
            return Page();
        }

        private void Sort(string sortOption)
        {
            Creators = sortOption switch
            {
                "byAlphabet" => Creators.OrderBy(c => c.Name).ToList(),
                _ => Creators.OrderBy(c => c.Id).ToList()
            };
        }
    }
}