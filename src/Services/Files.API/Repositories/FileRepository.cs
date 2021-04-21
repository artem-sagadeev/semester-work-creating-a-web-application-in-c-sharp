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
        private readonly IMongoCollection<Link> _links;

        public FileRepository()
        {
            const string connectionString = "mongodb://localhost:27017/files";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(connection.DatabaseName);
            _files = database.GetCollection<File>("Files");
            _links = database.GetCollection<Link>("Links");
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

        public async Task<Link> GetLinkAsync(string fileId, string token)
        {
            var builder = Builders<Link>.Filter;
            var filter = builder.Eq("FileId", fileId) & builder.Eq("Token", token);
            var links = await _links.FindAsync(filter);
            return await links.FirstAsync();
        }

        public async Task CreateLinkAsync(Link link)
        {
            await _links.InsertOneAsync(link);
        }

        public async Task DeleteLinkAsync(string id)
        {
            var filter = Builders<Link>.Filter.Eq("Id", id);
            await _links.FindOneAndDeleteAsync(filter);
        }
    }
}