using FSPBook.Core.Interfaces;
using FSPBook.Portal.Areas.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FSPBook.Portal.Areas.MVC.Controllers
{
    [Area("MVC")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly INewsService _newsService;

        public HomeController(IPostService postService, INewsService newsService)
        {
            _postService = postService;
            _newsService = newsService;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 1;
            var posts = await _postService.GetPostsAsync(pageNumber ?? 0, pageSize,0);

            // Fetch the latest 5 technology news articles, but max limit for artical is 3 for free account, and also source filter is restricted
            var news = await _newsService.GetLatestTechnologyNewsAsync(5,"cnet.com,qz.com");

            // Pass the news to the view
            ViewBag.TechnologyNews = news;

            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> LoadMorePosts ([FromBody] PostsRequestModel postsRequest) 
        {
            int pageSize = 1;
            var posts = await _postService.GetPostsAsync(postsRequest.PageNumber ?? 0, pageSize, postsRequest.LatestPostId ?? 0);
            return PartialView(posts);
        }
    }
}
