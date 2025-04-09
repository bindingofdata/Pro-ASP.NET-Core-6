using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    public sealed class TimeTagHelperComponent : TagHelperComponent
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string timestamp = DateTime.Now.ToLongTimeString();

            if (string.Equals(output.TagName, "body"))
            {
                TagBuilder elem = new TagBuilder("div");
                elem.Attributes.Add("class", "bg-info text-white m-2 p-2");
                elem.InnerHtml.AppendHtml($"Time: {timestamp}");
                output.PreContent.AppendHtml(elem);
            }
        }
    }
}
