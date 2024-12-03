using FSPBook.Core.Interfaces;
using FSPBook.Pages;
using FSPBook.Portal.Areas.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FSPBook.Portal.Areas.MVC.Controllers
{
    [Area("MVC")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ErrorModel> _logger;

        public ProfileController(IProfileService profileService, ILogger<ErrorModel> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int id, int pageSize = Constants.PageSize)
        {
            try
            {
                //throw new Exception("error");
                var profile = await _profileService.GetProfileAsync(id);
                if (profile == null)
                    throw new Exception("User not found");
                profile.Posts = await _profileService.GetUserPostsAsync(id, 0, 0, pageSize);
                return View(profile);
            }
            catch(Exception ex)
            {
                // Log the exception 
                _logger.LogError(ex, "An error occurred while processing the request.");

                // Generate a RequestId for tracking
                ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                ViewData["ErrorMessage"] = ex.Message;

                // Render the Error view
                return View("Error");
            }            
        }

        [HttpPost]
        public async Task<IActionResult> LoadMorePosts([FromBody] PostsRequestModel postsRequest)
        {
            try
            {
                //throw new Exception("error");
                var posts = await _profileService.GetUserPostsAsync(postsRequest.UserId ?? 0, postsRequest.LatestPostId ?? 0, postsRequest.PageNumber ?? 0, postsRequest.PageSize ?? 1);
                return PartialView("_PostListPartial", posts);
            }
            catch (Exception ex)
            {
                // Log the exception 
                _logger.LogError(ex, "An error occurred while processing the request.");

                var data = new ErrorResponse
                {
                    Status = false,
                    Message = "Something went wrong while processing the request"
                };
                return Json(data);
            }
            
        }
    }
}
