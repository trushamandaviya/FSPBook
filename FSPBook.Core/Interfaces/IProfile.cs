
using FSPBook.Core.Models;

namespace FSPBook.Core.Interfaces
{
    public interface IProfile
    {
        Task<ProfileModel> GetProfileAsync(int userId);
        Task<List<PostModel>> GetUserPostsAsync(int userId, int latestPostId, int pageNumber, int pageSize);
    }
}
