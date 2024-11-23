using FSPBook.Core.Models;

namespace FSPBook.Core.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileModel> GetProfileAsync(int userId);
        Task<List<PostModel>> GetUserPostsAsync(int userId, int latestPostId, int pageNumber, int pageSize);
    }
}
