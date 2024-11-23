using Microsoft.AspNetCore.Mvc;

namespace FSPBook.Portal.Areas.MVC.Controllers
{
    [Area("MVC")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
