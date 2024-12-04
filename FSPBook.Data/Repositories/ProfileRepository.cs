using FSPBook.Core.Interfaces;
using FSPBook.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSPBook.Data.Repositories
{
    public class ProfileRepository : IProfile
    {
        private readonly Context _context;

        public ProfileRepository(Context context)
        {
            _context = context;
        }

        public async Task<ProfileModel> GetProfileAsync(int userId)
        {
            var user = await _context.Profile.FirstOrDefaultAsync(p => p.Id == userId);
            if (user == null) return null;

            return new ProfileModel
            {
                Id = user.Id,
                Name = user.FullName,
                JobTitle = user.JobTitle
            };
        }
        
    }
}
