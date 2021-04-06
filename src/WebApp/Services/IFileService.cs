using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IFileService
    {
        Task<string> GetLink(string id);
    }
}