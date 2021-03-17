using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly HttpClient _client;

        public DeveloperService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var response = await _client.GetAsync($"/Developers/GetUsers");
            return await response.ReadContentAs<IEnumerable<UserModel>>();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var response = await _client.GetAsync($"/Developers/GetUser/{id}");
            return await response.ReadContentAs<UserModel>();
        }

        public async Task<IEnumerable<ProjectModel>> GetUserProjects(int userId)
        {
            var response = await _client.GetAsync($"/Developers/GetUserProjects?userId={userId}");
            return await response.ReadContentAs<IEnumerable<ProjectModel>>();
        }

        public async Task<IEnumerable<CompanyModel>> GetUserCompanies(int userId)
        {
            var response = await _client.GetAsync($"/Developers/GetUserCompanies?userId={userId}");
            return await response.ReadContentAs<IEnumerable<CompanyModel>>();
        }

        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            var response = await _client.GetAsync($"/Developers/GetProjects");
            return await response.ReadContentAs<IEnumerable<ProjectModel>>();
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            var response = await _client.GetAsync($"/Developers/GetProject/{id}");
            return await response.ReadContentAs<ProjectModel>();
        }

        public async Task<IEnumerable<UserModel>> GetProjectUsers(int projectId)
        {
            var response = await _client.GetAsync($"/Developers/GetProjectUsers?projectId={projectId}");
            return await response.ReadContentAs<IEnumerable<UserModel>>();
        }

        public async Task<CompanyModel> GetProjectCompany(int projectId)
        {
            var response = await _client.GetAsync($"/Developers/GetProjectCompany?projectId={projectId}");
            return await response.ReadContentAs<CompanyModel>();
        }

        public async Task<IEnumerable<CompanyModel>> GetCompanies()
        {
            var response = await _client.GetAsync($"/Developers/GetCompanies");
            return await response.ReadContentAs<IEnumerable<CompanyModel>>();
        }

        public async Task<CompanyModel> GetCompany(int id)
        {
            var response = await _client.GetAsync($"/Developers/GetCompany/{id}");
            return await response.ReadContentAs<CompanyModel>();
        }

        public async Task<IEnumerable<UserModel>> GetCompanyUsers(int companyId)
        {
            var response = await _client.GetAsync($"/Developers/GetCompanyUsers?companyId={companyId}");
            return await response.ReadContentAs<IEnumerable<UserModel>>();
        }

        public async Task<IEnumerable<ProjectModel>> GetCompanyProjects(int companyId)
        {
            var response = await _client.GetAsync($"/Developers/GetCompanyProjects?companyId={companyId}");
            return await response.ReadContentAs<IEnumerable<ProjectModel>>();
        }
    }
}