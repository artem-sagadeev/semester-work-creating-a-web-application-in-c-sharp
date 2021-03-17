using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IDeveloperService
    {
        //Users
        Task<IEnumerable<UserModel>> GetUsers();
        Task<UserModel> GetUser(int id);
        Task<IEnumerable<ProjectModel>> GetUserProjects(int userId);
        Task<IEnumerable<CompanyModel>> GetUserCompanies(int userId);

        //Projects
        Task<IEnumerable<ProjectModel>> GetProjects();
        Task<ProjectModel> GetProject(int id);
        Task<IEnumerable<UserModel>> GetProjectUsers(int projectId);
        Task<CompanyModel> GetProjectCompany(int projectId);
        
        //Companies
        Task<IEnumerable<CompanyModel>> GetCompanies();
        Task<CompanyModel> GetCompany(int id);
        Task<IEnumerable<UserModel>> GetCompanyUsers(int companyId);
        Task<IEnumerable<ProjectModel>> GetCompanyProjects(int companyId);
    }
}