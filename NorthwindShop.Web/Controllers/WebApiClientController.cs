using Microsoft.AspNetCore.Mvc;

namespace NorthwindShop.Web.Controllers
{
    public class WebApiClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
