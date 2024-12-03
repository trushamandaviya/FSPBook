using Microsoft.EntityFrameworkCore;
using FSPBook.Data;
using FSPBook.Core.Services;
using FSPBook.Data.Entities;
using FSPBook.Data.Repositories;
using FSPBook.Portal.Areas.MVC.Models;

namespace FSPBook.Tests.Services
{
    public class PostServiceTests
    {
        private readonly PostService _postService;
        private readonly DbContextOptions<Context> _options;

        public PostServiceTests()
        {
            // Configure in-memory database
            _options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "FSPBookDataBase")
                .Options;

            // Seed test data
            using (var context = new Context(_options))
            {
                if (context.Profile.Count() == 0)
                {
                    var profiles = new List<Profile> {
                    new Profile { Id = 1, FirstName = "Jane", LastName = "Doe", JobTitle = "Developer" },
                    new Profile { Id = 2, FirstName = "John", LastName = "Smith", JobTitle = "Consultant" },
                    new Profile { Id = 3, FirstName = "Maisie", LastName = "Jones", JobTitle = "Project Manager" },
                    new Profile { Id = 4, FirstName = "Jack", LastName = "Simpson", JobTitle = "Engagement Officer" },
                    new Profile { Id = 5, FirstName = "Sadie", LastName = "Williams", JobTitle = "Finance Director" },
                    new Profile { Id = 6, FirstName = "Pete", LastName = "Jackson", JobTitle = "Developer" },
                    new Profile { Id = 7, FirstName = "Sinead", LastName = "O'Leary", JobTitle = "Consultant" }
                };
                    context.Profile.AddRange(profiles);
                }

                if (context.Post.Count() == 0)
                {
                    var posts = new List<Post>
                    {
                        new Post { Id = 1, Content = "Post 1", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 1 },
                        new Post { Id = 2, Content = "Post 2", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 2 },
                        new Post { Id = 3, Content = "Post 3", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 1 },
                        new Post { Id = 4, Content = "Post 4", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 2 },
                        new Post { Id = 5, Content = "Post 5", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 3 },
                        new Post { Id = 6, Content = "Post 6", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 4 },
                        new Post { Id = 7, Content = "Post 7", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 5 },
                        new Post { Id = 8, Content = "Post 8", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 6 },
                        new Post { Id = 9, Content = "Post 9", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 6 },
                        new Post { Id = 10, Content = "Post 10", DateTimePosted = new DateTimeOffset(2020, 11, 1, 10, 0, 0, TimeSpan.Zero), AuthorId = 7 },
                        new Post { Id = 11, Content = "Post 11", DateTimePosted = new DateTimeOffset(2020, 11, 1, 15, 0, 0, TimeSpan.Zero), AuthorId = 7 },
                    };
                    context.Post.AddRange(posts);
                }

                context.SaveChanges();
                //var profile2 = context.Profile.ToList();
                //var post2 = context.Post.ToList();
            }

            // Create PostService instance
            var testContext = new Context(_options);

            // Create an instance of the repository
            var postRepository = new PostRepository(testContext);

            // Pass the repository to the service
            _postService = new PostService(postRepository);

        }

        [Fact]
        public async Task GetPostsAsync_ShouldReturnPagedPosts()
        {
            // Arrange
            int pageNumber = 0;
            int pageSize = Constants.PageSize;

            // Act
            var posts = await _postService.GetPostsAsync(pageNumber, pageSize, 0);

            // Assert
            Assert.NotNull(posts);
            Assert.Equal(Constants.PageSize, posts.Count); // Check that only 2 posts are returned
        }

        [Fact]
        public async Task GetPostsAsync_ShouldReturnEmpty_WhenNoPosts()
        {
            // Arrange
            using (var context = new Context(_options))
            {
                context.Post.RemoveRange(context.Post);
                context.SaveChanges();
            }

            // Act
            var posts = await _postService.GetPostsAsync(0, 2, 0);

            // Assert
            Assert.Empty(posts);
        }

    }
}