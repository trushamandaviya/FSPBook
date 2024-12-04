using FSPBook.Core.Models;

namespace FSPBook.Core.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileModel> GetProfileAsync(int userId);
    }
}
