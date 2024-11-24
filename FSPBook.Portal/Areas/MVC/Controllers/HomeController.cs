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

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 1;
            var posts = await _postService.GetPostsAsync(pageNumber ?? 0, pageSize,0);
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
