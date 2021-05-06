using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models.Files;

namespace WebApp.Services.Files
{
    public class FileService : IFileService
    {
        private readonly HttpClient _client;

        public FileService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<FileModel>> GetPostFiles(int postId)
        {
            var response = await _client.GetAsync($"/Files/GetPostFiles/{postId}");
            return await response.ReadContentAs<IEnumerable<FileModel>>();
        }

        public async Task<FileModel> GetFile(string id)
        {
            var response = await _client.GetAsync($"/Files/GetLink/{id}");
            return await response.ReadContentAs<FileModel>();
        }

        public async Task<AvatarModel> GetAvatar(int creatorId, CreatorType creatorType)
        {
            var response = await _client.GetAsync($"/Avatars/Get?creatorId={creatorId}&creatorType={creatorType}");
            return await response.ReadContentAs<AvatarModel>();
        }

        public async Task CreateAvatar(AvatarModel avatar)
        {
            await _client.PostAsJsonAsync("/Avatars/Create", avatar);
        }

        public async Task CreateCover(CoverModel cover)
        {
            await _client.PostAsJsonAsync("/Covers/Create", cover);
        }

        public async Task CreateFile(FileModel file)
        {
            await _client.PostAsJsonAsync("/Files/Create", file);
        }

        public async Task<CoverModel> GetCover(int postId)
        {
            var response = await _client.GetAsync($"/Covers/Get?postId={postId}");
            return await response.ReadContentAs<CoverModel>();
        }
    }
}