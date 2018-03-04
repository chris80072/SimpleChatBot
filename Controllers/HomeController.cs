using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // var message = new MessageModel();
        // return View(model: message);
        return View();
    }
}   