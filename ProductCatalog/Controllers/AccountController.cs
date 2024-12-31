using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.PL.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
