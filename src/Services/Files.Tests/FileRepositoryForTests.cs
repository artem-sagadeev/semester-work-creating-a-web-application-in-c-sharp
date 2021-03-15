using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Files.API.Entities;
using Files.API.Repositories;

namespace Files.Tests
{
    public class FileRepositoryForTests : IFileRepository
    {
        private readonly List<FileInfo> _files = new List<FileInfo>()
        {
            new("NewFile1.txt") {Id = "1"},
            new("NewFile2.txt") {Id = "2"},
            new("Newfile3.txt") {Id = "3"}
        };
        
        public Task<FileInfo> GetFileAsync(string id)
        {
            return Task<FileInfo>.Factory.StartNew(() =>
            {
                return _files.First(f => f.Id == id);
            });
        }

        public Task CreateFileAsync(FileInfo file)
        {
            return Task.Factory.StartNew(() =>
            {
                _files.Add(file);
            });
        }

        public Task DeleteFileAsync(string id)
        {
            return Task.Factory.StartNew(() =>
            {
                var file = _files.First(f => f.Id == id);
                _files.Remove(file);
            });
        }
    }
}