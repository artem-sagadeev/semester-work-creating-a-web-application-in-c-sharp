using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Files.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Files.API.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly IMongoCollection<File> _files;
        private readonly IMongoCollection<Avatar> _avatars;

        public FileRepository()
        {
            const string connectionString = "mongodb://localhost:27017/files";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(connection.DatabaseName);
            _files = database.GetCollection<File>("Files");
            _avatars = database.GetCollection<Avatar>("Avatars");
        }

        public async Task<File> GetFileAsync(string id)
        {
            var filter = Builders<File>.Filter.Eq("Id", id);
            var files = await _files.FindAsync(filter);
            return await files.FirstAsync();
        }
        
        public async Task<IEnumerable<File>> GetPostFiles(int postId)
        {
            var filter = Builders<File>.Filter.Eq("PostId", postId);
            var files = await _files.FindAsync(filter);
            return await files.ToListAsync();
        }

        public async Task CreateFileAsync(File file)
        {
            await _files.InsertOneAsync(file);
        }

        public async Task DeleteFileAsync(string id)
        {
            var filter = Builders<File>.Filter.Eq("Id", id);
            await _files.FindOneAndDeleteAsync(filter);
        }

        public async Task<Avatar> GetAvatarAsync(int creatorId)
        {
            var filter = Builders<Avatar>.Filter.Eq("CreatorId", creatorId);
            var avatar = await _avatars.FindAsync(filter);
            return await avatar.FirstAsync();
        }

        public async Task CreateAvatarAsync(Avatar avatar)
        {
            await DeleteAvatarAsync(avatar.CreatorId);
            await _avatars.InsertOneAsync(avatar);
        }

        public async Task DeleteAvatarAsync(int creatorId)
        {
            var filter = Builders<Avatar>.Filter.Eq("CreatorId", creatorId);
            await _avatars.FindOneAndDeleteAsync(filter);
        }
    }
}