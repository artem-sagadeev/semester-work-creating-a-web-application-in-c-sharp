using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApp.Extensions;
using WebApp.Models;

namespace WebApp.Services
{
    public class PostsService : IPostsService
    {
        private readonly HttpClient _client;

        public PostsService(HttpClient client)
        {
            _client = client;
        }

        public async Task<PostModel> GetPost(int id)
        {
            var response = await _client.GetAsync($"/Posts/GetPost/{id}");
            return await response.ReadContentAs<PostModel>();
        }

        public async Task<IEnumerable<PostModel>> GetUserPosts(int userId)
        {
            var response = await _client.GetAsync($"/Posts/GetUserPosts?userId={userId}");
            return await response.ReadContentAs<IEnumerable<PostModel>>();
        }

        public async Task<IEnumerable<PostModel>> GetGroupPosts(int groupId)
        {
            var response = await _client.GetAsync($"/Posts/GetGroupPosts?groupId={groupId}");
            return await response.ReadContentAs<IEnumerable<PostModel>>();
        }

        public async Task<CommentModel> GetComment(int id)
        {
            var response = await _client.GetAsync($"/Posts/GetComment/{id}");
            return await response.ReadContentAs<CommentModel>();
        }

        public async Task<IEnumerable<CommentModel>> GetUserComments(int userId)
        {
            var response = await _client.GetAsync($"/Posts/GetUserComments?userId={userId}");
            return await response.ReadContentAs<IEnumerable<CommentModel>>();
        }

        public async Task<IEnumerable<CommentModel>> GetPostComments(int postId)
        {
            var response = await _client.GetAsync($"/Posts/GetPostComments?postId={postId}");
            return await response.ReadContentAs<IEnumerable<CommentModel>>();
        }
    }
}