using System.Threading.Tasks;

namespace WebApp.Services.Files
{
    public interface IFileService
    {
        Task<string> GetLink(string id);
    }
}