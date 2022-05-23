using System;
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
        
        private readonly Avatar _defaultUserAvatar = new Avatar(0, "defaultUserAvatar.jpg", 0);
        private readonly Avatar _defaultProjectAvatar = new Avatar(0, "defaultProjectAvatar.jpg", 1);
        private readonly Avatar _defaultCompanyAvatar = new Avatar(0, "defaultCompanyAvatar.jpg", 2);
        private readonly Cover _defaultCover = new Cover(0, "defaultCover.jpg");

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
            var avatar = await _context.Avatar
                .FirstOrDefaultAsync(avatar => avatar.CreatorId == creatorId && avatar.CreatorType == creatorType);
            
            return avatar ?? creatorType switch
            {
                CreatorType.User => _defaultUserAvatar,
                CreatorType.Project => _defaultProjectAvatar,
                CreatorType.Company => _defaultCompanyAvatar,
                _ => throw new ArgumentException()
            };
        }

        public async Task CreateAvatarAsync(Avatar avatar)
        {
            await DeleteAvatarAsync(avatar);
            _context.Avatar.Add(avatar);
            await _context.SaveChangesAsync();
        }
        
        private async Task DeleteAvatarAsync(Avatar avatar)
        {
            var avatars = await _context.Avatar
                .Where(x => x.CreatorId == avatar.CreatorId && x.CreatorType == avatar.CreatorType)
                .ToListAsync();

            _context.Avatar.RemoveRange(avatars);
        }

        public async Task CreateCoverAsync(Cover cover)
        {
            _context.Cover.Add(cover);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoverAsync(int postId)
        {
            var cover = await _context.Cover
                .Where(x => x.PostId == postId)
                .FirstOrDefaultAsync();
            
            if (cover is null)
                return;
            
            _context.Cover.Remove(cover);
            await _context.SaveChangesAsync();
        }

        public async Task<Cover> GetCoverAsync(int postId)
        {
            var cover = await _context.Cover
                .FirstOrDefaultAsync(cover => cover.PostId == postId);
            
            return cover ?? _defaultCover;
        }
    }
}