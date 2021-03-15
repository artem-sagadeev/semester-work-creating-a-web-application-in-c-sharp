using System.Threading.Tasks;
using Files.API.Entities;
using MongoDB.Driver;

namespace Files.API.Repositories
{
    public interface IFileRepository
    {
        public Task<FileInfo> GetFileAsync(string id);
        public Task CreateFileAsync(FileInfo file);
        public Task DeleteFileAsync(string id);
    }
}