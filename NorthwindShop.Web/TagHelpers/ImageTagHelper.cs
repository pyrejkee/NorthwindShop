using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthwindShop.Web.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "northwind-id")]
    public class ImageTagHelper : TagHelper
    {
        public int NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "btn btn-primary");
            output.Attributes.SetAttribute("href", $"/images/{NorthwindId}");
        }
    }
}
