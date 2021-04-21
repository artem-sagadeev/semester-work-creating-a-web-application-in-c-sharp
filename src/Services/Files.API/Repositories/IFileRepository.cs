using System.Collections.Generic;
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
        public Task<IEnumerable<File>> GetPostFiles(int postId);
        public Task<Avatar> GetAvatarAsync(int creatorId);
        public Task CreateAvatarAsync(Avatar avatar);
        public Task DeleteAvatarAsync(int creatorId);
    }
}