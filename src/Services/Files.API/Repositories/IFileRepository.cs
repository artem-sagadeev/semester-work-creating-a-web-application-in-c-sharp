using System.Threading.Tasks;
using Files.API.Entities;
using MongoDB.Driver;

namespace Files.API.Repositories
{
    public interface IFileRepository
    {
        public Task<File> GetFileAsync(string id);
        public Task CreateFileAsync(File file);
        public Task DeleteFileAsync(string id);
        public Task<Link> GetLinkAsync(string fileId, string token);
        public Task CreateLinkAsync(Link link);
        public Task DeleteLinkAsync(string id);
    }
}