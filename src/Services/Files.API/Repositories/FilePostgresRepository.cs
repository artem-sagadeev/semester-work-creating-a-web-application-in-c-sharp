using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Files.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Files.API.Repositories
{
    public class FilePostgresRepository : IFileRepository
    {
        private readonly FilesDbContext _context;

        public FilePostgresRepository(FilesDbContext context)
        {
            _context = context;
        }

        public async Task<File> GetFileAsync(string id)
        {
            return await _context.File.FindAsync(id);
        }

        public async Task CreateFileAsync(File file)
        {
            _context.File.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFileAsync(string id)
        {
            _context.File.Remove(await GetFileAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<File>> GetPostFiles(int postId)
        {
            return await _context
                .File
                .Where(file => file.PostId == postId)
                .ToListAsync();
        }

        public async Task<Avatar> GetAvatarAsync(int creatorId, CreatorType creatorType)
        {
            return await _context
                .Avatar
                .FirstAsync(avatar => avatar.CreatorId == creatorId && avatar.CreatorType == creatorType);
        }

        public async Task CreateAvatarAsync(Avatar avatar)
        {
            _context.Avatar.Add(avatar);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCoverAsync(Cover cover)
        {
            _context.Cover.Add(cover);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoverAsync(int postId)
        {
            _context.Cover.Remove(await GetCoverAsync(postId));
            await _context.SaveChangesAsync();
        }

        public async Task<Cover> GetCoverAsync(int postId)
        {
            return await _context
                .Cover
                .FirstAsync(cover => cover.PostId == postId);
        }
    }
}