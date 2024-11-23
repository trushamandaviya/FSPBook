using FSPBook.Core.Interfaces;
using FSPBook.Core.Models;

namespace FSPBook.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPost _postRepository;

        public PostService(IPost postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostModel>> GetPostsAsync(int pageNumber, int pageSize, int latestPostId)
        {
            return await _postRepository.GetPostsAsync(pageNumber, pageSize, latestPostId);
        }
    }
}
