using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models.Files;

namespace WebApp.Services.Files
{
    public interface IFileService
    {
        Task<IEnumerable<FileModel>> GetPostFiles(int postId);
        Task<FileModel> GetFile(string id);
        Task<AvatarModel> GetAvatar(int creatorId, CreatorType creatorType);
        Task CreateAvatar(AvatarModel avatar);
        Task CreateCover(CoverModel cover);
        Task CreateFile(FileModel file);
        Task<CoverModel> GetCover(int postId);
    }
}