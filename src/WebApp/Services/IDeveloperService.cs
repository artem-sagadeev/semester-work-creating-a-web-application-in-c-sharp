using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IDeveloperService
    {
        //Users
        Task<IEnumerable<UserModel>> GetUsers();
        Task<IEnumerable<UserModel>> GetUsersByName(string name);
        Task<UserModel> GetUser(int id);
        Task<IEnumerable<ProjectModel>> GetUserProjects(int userId);
        Task<IEnumerable<CompanyModel>> GetUserCompanies(int userId);

        //Projects
        Task<IEnumerable<ProjectModel>> GetProjects();
        Task<IEnumerable<ProjectModel>> GetProjectsByName(string name);
        Task<ProjectModel> GetProject(int id);
        Task<IEnumerable<UserModel>> GetProjectUsers(int projectId);
        Task<CompanyModel> GetProjectCompany(int projectId);
        
        //Companies
        Task<IEnumerable<CompanyModel>> GetCompanies();
        Task<IEnumerable<CompanyModel>> GetCompaniesByName(string name);
        Task<CompanyModel> GetCompany(int id);
        Task<IEnumerable<UserModel>> GetCompanyUsers(int companyId);
        Task<IEnumerable<ProjectModel>> GetCompanyProjects(int companyId);
        
        //Tags
        Task<IEnumerable<TagModel>> GetTags(ICreator creator);
    }
}