using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models.Files;

namespace WebApp.Services.Files
{
    public interface IFileService
    {
        Task<IEnumerable<FileModel>> GetPostFiles(int postId);
        Task<string> GetLink(string id);
    }
}