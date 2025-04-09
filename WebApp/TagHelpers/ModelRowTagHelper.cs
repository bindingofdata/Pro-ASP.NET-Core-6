using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("tr", Attributes = "for")]
    public sealed class ModelRowTagHelper : TagHelper
    {
        public string Format { get; set; } = string.Empty;
        public ModelExpression? For { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;

            TagBuilder th = new TagBuilder("th");
            th.InnerHtml.Append(For?.Name ?? string.Empty);
            output.Content.AppendHtml(th);

            TagBuilder td = new TagBuilder("td");
            if (!string.IsNullOrWhiteSpace(Format)
                && For?.Metadata.ModelType == typeof(decimal))
            {
                td.InnerHtml.Append(((decimal)For.Model).ToString(Format));
            }
            else
            {
                td.InnerHtml.Append(For?.Model.ToString() ?? string.Empty);
            }
            output.Content.AppendHtml(td);
        }
    }
}
