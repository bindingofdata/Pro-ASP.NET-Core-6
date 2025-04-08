using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    // Narrow tag helper scope example
    //[HtmlTargetElement("tr", Attributes = "bg-color,text-color", ParentTag = "thead")]

    // Widen tag helper scope to any elements example
    //[HtmlTargetElement("*", Attributes = "bg-color,text-color")]

    // Widen tag helper scope to specific elements example
    [HtmlTargetElement("tr", Attributes = "bg-color,text-color")]
    [HtmlTargetElement("td", Attributes = "bg-color")]
    public sealed class TrTagHelper : TagHelper
    {
        public string BgColor { get; set; } = "dark";
        public string TextColor { get; set; } = "white";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-{TextColor}");
        }
    }
}
