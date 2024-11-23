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

        public async Task<List<PostModel>> GetUserPostsAsync(int userId, int latestPostId, int pageNumber, int pageSize)
        {
            var query = _context.Post.AsQueryable();
            
            // Filter posts for the given user
            query = query.Where(p => p.AuthorId == userId);
           
            // Fetch posts older than the latest id
            if (latestPostId > 0)
            {
                query = query.Where(p => p.Id <= latestPostId);
            }

            var posts = await query
                .OrderByDescending(p => p.DateTimePosted)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Include(p => p.Author)
                .ToListAsync();

            // Map to PostModel
            return posts.Select(p => new PostModel
            {
                Id = p.Id,
                Content = p.Content,
                DateTimePosted = p.DateTimePosted
            }).ToList();
        }
    }
}
