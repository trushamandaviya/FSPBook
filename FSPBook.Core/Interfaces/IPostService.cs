using FSPBook.Core.Models;

namespace FSPBook.Core.Interfaces
{
    public interface IPostService
    {
        Task<List<PostModel>> GetPostsAsync(int pageNumber, int pageSize, int latestPostId, int userId = 0);
    }
}
