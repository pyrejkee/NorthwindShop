using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindShop.Web.ViewComponents
{
    public class BreadCrumb : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var breadCrumbData = GetBreadCrumbData();

            return View(breadCrumbData);
        }

        private Dictionary<string, string> GetBreadCrumbData()
        {
            Dictionary<string, string> breadCrumbData = new Dictionary<string, string>();
            breadCrumbData.Add("Home", "/");
            var routes = RouteData.Values.Values.Cast<string>().ToList();
            routes.RemoveAll(x => x.Contains("Index") || x.All(char.IsDigit));
            foreach (var route in routes)
            {
                breadCrumbData.Add(route, $"{breadCrumbData.Last().Value}{route}/");
            }

            return breadCrumbData;
        }
    }
}
