using Microsoft.AspNetCore.Mvc;

namespace website_e_commerce.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
