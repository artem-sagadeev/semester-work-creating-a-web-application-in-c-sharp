using System.Linq;
using System.Threading.Tasks;
using Files.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Files.API.Repositories
{
    public class FileRepository : IFileRepository
    {
        private IMongoCollection<FileInfo> Files;

        public FileRepository()
        {
            const string connectionString = "mongodb://localhost:27017/files";
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(connection.DatabaseName);
            Files = database.GetCollection<FileInfo>("Files");
        }

        public async Task<FileInfo> GetFileAsync(string id)
        {
            var filter = Builders<FileInfo>.Filter.Eq("Id", id);
            var files = await Files.FindAsync(filter);
            return await files.FirstAsync();
        }

        public async Task CreateFileAsync(FileInfo file)
        {
            await Files.InsertOneAsync(file);
        }

        public async Task DeleteFileAsync(string id)
        {
            var filter = Builders<FileInfo>.Filter.Eq("Id", id);
            await Files.FindOneAndDeleteAsync(filter);
        }
    }
}