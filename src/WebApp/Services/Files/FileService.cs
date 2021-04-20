using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Services.Files
{
    public class FileService : IFileService
    {
        private readonly HttpClient _client;

        public FileService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetLink(string id)
        {
            var response = await _client.GetAsync($"/Files/GetLink/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}