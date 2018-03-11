using Microsoft.AspNetCore.Mvc;

namespace SimpleChatBot.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }   
}
