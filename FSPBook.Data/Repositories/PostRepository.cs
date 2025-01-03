﻿using FSPBook.Core.Interfaces;
using FSPBook.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSPBook.Data.Repositories
{
    public class PostRepository : IPost
    {
        private readonly Context _context;

        public PostRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<PostModel>> GetPostsAsync(int pageNumber, int pageSize, int latestPostId, int userId = 0)
        {
            var query = _context.Post.AsQueryable();

            // Filter posts for the given user
            if (userId > 0)
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

            // Map EF Core entities to lightweight models
            return posts.Select(p => new PostModel
            {
                Id = p.Id,
                AuthorId = p.AuthorId,
                Content = p.Content,
                DateTimePosted = p.DateTimePosted,
                AuthorName = p.Author?.FullName ?? "Unknown"
            }).ToList();
        }
    }
}
