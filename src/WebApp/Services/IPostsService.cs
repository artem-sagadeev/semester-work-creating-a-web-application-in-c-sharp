using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IPostsService
    {
        public Task<PostModel> GetPost(int id);
        public Task<IEnumerable<PostModel>> GetUserPosts(int userId);
        public Task<IEnumerable<PostModel>> GetGroupPosts(int groupId);

        public Task<CommentModel> GetComment(int id);
        public Task<IEnumerable<CommentModel>> GetUserComments(int userId);
        public Task<IEnumerable<CommentModel>> GetPostComments(int postId);
    }
}