using Moq;
using FSPBook.Core.Models;
using FSPBook.Core.Interfaces;
using FSPBook.Portal.Areas.MVC.Controllers;
using FSPBook.Pages;
using Microsoft.Extensions.Logging;
using FSPBook.Portal.Areas.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace FSPBook.Tests.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IPostService> _mockPostService;
        private readonly Mock<INewsService> _mockNewsService;
        private readonly Mock<ILogger<ErrorModel>> _mockLoggerService;
        private readonly HomeController _controller;
        

        public HomeControllerTests()
        {
            // Mock the IPostService dependency
            _mockPostService = new Mock<IPostService>();
            _mockNewsService = new Mock<INewsService>();
            _mockLoggerService = new Mock<ILogger<ErrorModel>>();

            // Initialize the HomeController with the mock service
            _controller = new HomeController(_mockPostService.Object, _mockNewsService.Object, _mockLoggerService.Object);
        }

        [Fact]
        public async Task LoadMorePosts_ShouldReturnJson_WhenErrorOccurs()
        {
            // Arrange & Act
            var result = await _controller.LoadMorePosts(null);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result); // Ensure the result is a JsonResult
            var data = Assert.IsType<ErrorResponse>(jsonResult.Value); // Cast the value to ErrorResponse

            Assert.False(data.Status); // Check if Status is false
            Assert.Equal("Something went wrong while processing the request", data.Message);
        }

        [Fact]
        public async Task LoadMorePosts_ShouldReturnPosts_WhenNoErrorOccurs()
        {

            // Arrange
            var latestPostId = 1; // Only posts with Id <= 3 should be returned
            var pageSize = Constants.PageSize;

            var mockPosts = new List<PostModel>
            {
                new PostModel { Id = 1, Content = "Post 1", DateTimePosted = DateTime.Now.AddDays(-1) },
                new PostModel { Id = 2, Content = "Post 2", DateTimePosted = DateTime.Now.AddDays(-2) },
                new PostModel { Id = 3, Content = "Post 3", DateTimePosted = DateTime.Now.AddDays(-3) },
                new PostModel { Id = 4, Content = "Post 4", DateTimePosted = DateTime.Now.AddDays(-4) } // Should not be included
            };

            // Mock IPostService
            _mockPostService
                .Setup(s => s.GetPostsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int pageNumber, int pageSize, int latestId) =>
                {
                    return mockPosts.Where(p => p.Id <= latestId).Take(pageSize).ToList();
                });

            var request = new PostsRequestModel
            {
                PageNumber = 0,
                LatestPostId = latestPostId,
                PageSize = pageSize
            };

            // Act
            var result = await _controller.LoadMorePosts(request);

            // Assert
            var partialViewResult = Assert.IsType<PartialViewResult>(result); // Check that it returns a PartialViewResult
            var posts = Assert.IsAssignableFrom<List<PostModel>>(partialViewResult.Model); // Ensure the model is a list of posts

            Assert.All(posts, p => Assert.True(p.Id <= latestPostId, $"Post Id {p.Id} is greater than LatestPostId {latestPostId}"));

            // Verify the mock was called
            _mockPostService.Verify(s => s.GetPostsAsync(Convert.ToInt32(request.PageNumber), pageSize, latestPostId));


        }

    }
}