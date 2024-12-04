
using FSPBook.Core.Models;

namespace FSPBook.Core.Interfaces
{
    public interface IProfile
    {
        Task<ProfileModel> GetProfileAsync(int userId);
    }
}
