using FSPBook.Core.Interfaces;
using FSPBook.Portal.Areas.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FSPBook.Portal.Areas.MVC.Controllers
{
    [Area("MVC")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var profile = await _profileService.GetProfileAsync(id);
            if (profile == null)
                return NotFound();
            profile.Posts = await _profileService.GetUserPostsAsync(id, 0, 0, 1);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> LoadMorePosts([FromBody] PostsRequestModel postsRequest)
        {
            var posts = await _profileService.GetUserPostsAsync(postsRequest.UserId ?? 0, postsRequest.LatestPostId ?? 0, postsRequest.PageNumber ?? 0, postsRequest.PageSize ?? 1);
            return PartialView("_PostListPartial", posts);
        }
    }
}
