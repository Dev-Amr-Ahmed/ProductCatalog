using Microsoft.AspNetCore.Mvc;
using ProductCatalog.PL.Models;

namespace ProductCatalog.PL.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int statusCode, string? message = null)
        {
            var errorVM = new ErrorVM
            {
                StatusCode = statusCode,
                Message = message
            };
            return View(errorVM);
        }
    }
}
