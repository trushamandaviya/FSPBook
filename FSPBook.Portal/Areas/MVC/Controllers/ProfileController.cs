using FSPBook.Core.Interfaces;
using FSPBook.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        public async Task<IActionResult> LoadMorePosts(int userId, int latestPostId, int? pageSize, int? pageNumber)
        {
            var posts = await _profileService.GetUserPostsAsync(userId, latestPostId, pageNumber ?? 0, pageSize ?? 1);
            return PartialView("_PostListPartial", posts);
        }
    }
}
