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
            var response = await _client.GetAsync("/Developers/GetUsers");
            return await response.ReadContentAs<List<UserModel>>();
        }

        public async Task<UserModel> GetUser(int id)
        {
            var response = await _client.GetAsync($"/Developers/GetUser/{id}");
            return await response.ReadContentAs<UserModel>();
        }

        public async Task<IEnumerable<ProjectModel>> GetUserProjects(int userId)
        {
            var response = await _client.GetAsync($"/Developes/GetUserProjects/{userId}");
            return await response.ReadContentAs<List<ProjectModel>>();
        }

        public async Task<IEnumerable<CompanyModel>> GetUserCompanies(int userId)
        {
            var response = await _client.GetAsync($"/Developers/GetUserCompanies/{userId}");
            return await response.ReadContentAs<List<CompanyModel>>();
        }

        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> GetProjectUsers(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CompanyModel> GetProjectCompany(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CompanyModel>> GetCompanies()
        {
            throw new System.NotImplementedException();
        }

        public async Task<CompanyModel> GetCompany(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserModel>> GetCompanyUsers(int companyId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ProjectModel>> GetCompanyProjects(int companyId)
        {
            throw new System.NotImplementedException();
        }
    }
}