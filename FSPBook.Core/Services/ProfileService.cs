using FSPBook.Core.Interfaces;
using FSPBook.Core.Models;

namespace FSPBook.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfile _profileRepository;

        public ProfileService(IProfile profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<ProfileModel> GetProfileAsync(int userId)
        {
            return await _profileRepository.GetProfileAsync(userId);
        }

    }
}
