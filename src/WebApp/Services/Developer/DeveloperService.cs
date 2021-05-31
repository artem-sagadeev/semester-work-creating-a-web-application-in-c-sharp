using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models.Developer;
using WebApp.Pages;

namespace WebApp.Services.Developer
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

        public async Task<IEnumerable<UserModel>> GetUsersByName(string name)
        {
            var response = await _client.GetAsync($"/Developers/GetUsersByName?name={name}");
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

        public async Task CreateUser(UserModel user)
        {
            await _client.PostAsJsonAsync($"/Developers/CreateUser", user);
        }

        public async Task UpdateUser(UserModel user)
        {
            await _client.PostAsJsonAsync($"/Developers/UpdateUser", user);
        }

        public async Task<IEnumerable<ProjectModel>> GetProjects()
        {
            var response = await _client.GetAsync($"/Developers/GetProjects");
            return await response.ReadContentAs<IEnumerable<ProjectModel>>();
        }

        public async Task<IEnumerable<ProjectModel>> GetProjectsByName(string name)
        {
            var response = await _client.GetAsync($"/Developers/GetProjectsByName?name={name}");
            return await response.ReadContentAs<IEnumerable<ProjectModel>>();
        }

        public async Task<ProjectModel> GetProject(int id)
        {
            var response = await _client.GetAsync($"/Developers/GetProject/{id}");
            return await response.ReadContentAs<ProjectModel>();
        }

        public async Task<ProjectModel> GetProject(string name)
        {
            var response = await _client.GetAsync($"/Developers/GetProject/{name}");
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

        public async Task<string> CreateProject(ProjectForm projectForm)
        {
            var response = await _client.PostAsJsonAsync("/Developers/CreateProject", projectForm);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task UpdateProject(ProjectModel project)
        {
            await _client.PostAsJsonAsync("/Developers/UpdateProject", project);
        }

        public async Task<IEnumerable<CompanyModel>> GetCompanies()
        {
            var response = await _client.GetAsync($"/Developers/GetCompanies");
            return await response.ReadContentAs<IEnumerable<CompanyModel>>();
        }

        public async Task<IEnumerable<CompanyModel>> GetCompaniesByName(string name)
        {
            var response = await _client.GetAsync($"/Developers/GetCompaniesByName?name={name}");
            return await response.ReadContentAs<IEnumerable<CompanyModel>>();
        }

        public async Task<CompanyModel> GetCompany(int id)
        {
            var response = await _client.GetAsync($"/Developers/GetCompany/{id}");
            return await response.ReadContentAs<CompanyModel>();
        }

        public async Task<CompanyModel> GetCompany(string name)
        {
            var response = await _client.GetAsync($"/Developers/GetCompany/{name}");
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

        public async Task<string> CreateCompany(CompanyForm companyForm)
        {
            var response = await _client.PostAsJsonAsync($"/Developers/CreateCompany", companyForm);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task UpdateCompany(CompanyModel company)
        {
            await _client.PostAsJsonAsync("/Developers/UpdateCompany", company);
        }

        public async Task<IEnumerable<TagModel>> GetTags(ICreator creator)
        {
            return creator switch
            {
                UserModel => await GetUserTags(creator.Id),
                ProjectModel => await GetProjectTags(creator.Id),
                CompanyModel => await GetCompanyTags(creator.Id),
                _ => throw new NotSupportedException()
            };
        }

        private async Task<IEnumerable<TagModel>> GetUserTags(int userId)
        {
            var response = await _client.GetAsync($"/Developers/GetUserTags?userId={userId}");
            return await response.ReadContentAs<IEnumerable<TagModel>>();
        }
        
        private async Task<IEnumerable<TagModel>> GetProjectTags(int projectId)
        {
            var response = await _client.GetAsync($"/Developers/GetProjectTags?projectId={projectId}");
            return await response.ReadContentAs<IEnumerable<TagModel>>();
        }
        
        private async Task<IEnumerable<TagModel>> GetCompanyTags(int companyId)
        {
            var response = await _client.GetAsync($"/Developers/GetCompanyTags?companyId={companyId}");
            return await response.ReadContentAs<IEnumerable<TagModel>>();
        }
    }
}