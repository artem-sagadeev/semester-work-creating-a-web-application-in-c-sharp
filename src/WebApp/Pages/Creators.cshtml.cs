using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;
using WebApp.Models.Developer;
using WebApp.Services;
using WebApp.Services.Developer;

namespace WebApp.Pages
{
    public class Creators : PageModel
    {
        private readonly IDeveloperService _developerService;

        public Creators(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public List<ICreator> CreatorModels { get; private set; }

        public async Task<ActionResult> OnGetAsync(string needUsers, 
            string needProjects, 
            string needCompanies, 
            string searchString,
            string sortOption)
        {
            CreatorModels = new List<ICreator>();

            var needAll = needUsers is null &&
                          needProjects is null &&
                          needCompanies is null;
            
            if (needUsers == "on" || needAll)
                CreatorModels.AddRange(searchString == null ? 
                    await _developerService.GetUsers() : 
                    await _developerService.GetUsersByName(searchString));
            
            if (needProjects == "on" || needAll)
                CreatorModels.AddRange(searchString == null ? 
                    await _developerService.GetProjects() : 
                    await _developerService.GetProjectsByName(searchString));
            
            if (needCompanies == "on" || needAll)
                CreatorModels.AddRange(searchString == null ? 
                    await _developerService.GetCompanies() : 
                    await _developerService.GetCompaniesByName(searchString));
            
            if (sortOption != null)
                Sort(sortOption);

            foreach (var creator in CreatorModels)
            {
                creator.Tags = await _developerService.GetTags(creator);

                switch (creator)
                {
                    case UserModel user:
                        user.Companies = await _developerService.GetUserCompanies(user.Id);
                        break;
                    case ProjectModel project:
                        project.Company = await _developerService.GetProjectCompany(project.Id);
                        break;
                }
            }
            
            return Page();
        }

        private void Sort(string sortOption)
        {
            CreatorModels = sortOption switch
            {
                "byAlphabet" => CreatorModels.OrderBy(c => c.Name).ToList(),
                _ => CreatorModels.OrderBy(c => c.Id).ToList()
            };
        }
    }
}