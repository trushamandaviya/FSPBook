
using FSPBook.Core.Models;

namespace FSPBook.Core.Interfaces
{
    public interface IPost
    {
        Task<List<PostModel>> GetPostsAsync(int pageNumber, int pageSize, int latestPostId);
    }
}
