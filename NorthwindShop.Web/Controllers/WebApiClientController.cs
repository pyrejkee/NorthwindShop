using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindShop.Web.Controllers
{
    [Authorize]
    public class WebApiClientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
