using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Text.Encodings.Web;

namespace NorthwindShop.Web.TagHelpers
{
    public static class ImageHtmlHelper
    {
        public static IHtmlContent NorthwindImageLink(
            this IHtmlHelper htmlHelper, int categoryId, string linkText)
        {
            var anchor = htmlHelper.RouteLink(linkText, new { id = categoryId }, new { @class = "btn btn-primary" });
            var writer = new StringWriter();
            anchor.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.GetStringBuilder().ToString());
        }
    }
}
