using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.DTOs;
using WebApp.Models.Posts;
using WebApp.Models.Subscription;

namespace WebApp.Services.Posts
{
    public interface IPostsService
    {
        public Task<PostModel> GetPost(int id);
        public Task<IEnumerable<PostModel>> GetUserPosts(int userId);
        public Task<IEnumerable<PostModel>> GetProjectPosts(int projectId);

        public Task<CommentModel> GetComment(int id);
        public Task<IEnumerable<CommentModel>> GetUserComments(int userId);
        public Task<IEnumerable<CommentModel>> GetPostComments(int postId);

        public Task<PostModel> CreatePost(PostModel post);
        public Task UpdateRequiredType(RequiredTypeDto dto);
        public Task UpdateText(TextDto dto);
    }
}