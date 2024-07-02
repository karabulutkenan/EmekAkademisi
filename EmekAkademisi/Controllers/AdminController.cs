using Microsoft.AspNetCore.Mvc;

namespace EmekAkademisi.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

