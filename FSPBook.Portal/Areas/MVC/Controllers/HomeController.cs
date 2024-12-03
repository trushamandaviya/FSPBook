﻿using FSPBook.Core.Interfaces;
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
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly INewsService _newsService;
        private readonly ILogger<ErrorModel> _logger;

        public HomeController(IPostService postService, INewsService newsService, ILogger<ErrorModel> logger)
        {
            _postService = postService;
            _newsService = newsService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            try
            {
                //throw new Exception("Test error");
                int pageSize = 1;
                var posts = await _postService.GetPostsAsync(pageNumber ?? 0, pageSize, 0);

                // Fetch the latest 5 technology news articles, but max limit for artical is 3 for free account, and also source filter is restricted
                var news = await _newsService.GetLatestTechnologyNewsAsync(5, "cnet.com,qz.com");

                // Pass the news to the view
                ViewBag.TechnologyNews = news;                
                return View(posts);
            }
            catch(Exception ex)
            {
                // Log the exception 
                _logger.LogError(ex, "An error occurred while processing the request.");

                // Generate a RequestId for tracking
                ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

                // Render the Error view
                return View("Error");
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> LoadMorePosts ([FromBody] PostsRequestModel postsRequest) 
        {
            try
            {
                int pageSize = 1;
                var posts = await _postService.GetPostsAsync(postsRequest.PageNumber ?? 0, pageSize, postsRequest.LatestPostId ?? 0);
                //throw new Exception("Test error"); 
                return PartialView("_LoadMorePosts", posts);
            }
            catch (Exception ex)
            {
                // Log the exception 
                _logger.LogError(ex, "An error occurred while processing the request.");

                var data = new
                {
                    Status = false,
                    Message = "Something went wrong while processing the request"
                };
                return Json(data);
            }            
        }
    }
}
