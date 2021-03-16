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
            var response = await _client.GetAsync("/Users/Get");
            return await response.ReadContentAs<List<UserModel>>();
        }

        public async Task<UserModel> GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<ProjectModel>> GetUserProjects(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CompanyModel>> GetUserCompanies(int userId)
        {
            throw new System.NotImplementedException();
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
    }
}