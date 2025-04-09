using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApp.TagHelpers
{
    [HtmlTargetElement("table")]
    public sealed class TableFooterSelector : TagHelperComponentTagHelper
    {
        public TableFooterSelector(ITagHelperComponentManager manager, ILoggerFactory loggerFactory)
            : base(manager, loggerFactory) { }
    }

    public sealed class TableFooterTagHelperComponent : TagHelperComponent
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.Equals(output.TagName, "table"))
            {
                TagBuilder cell = new TagBuilder("td");
                cell.Attributes.Add("colspan", "3");
                cell.Attributes.Add("class", "bg-dark text-white text-center");
                cell.InnerHtml.Append("Table footer");

                TagBuilder row = new TagBuilder("tr");
                row.InnerHtml.AppendHtml(cell);

                TagBuilder footer = new TagBuilder("tfoot");
                footer.InnerHtml.AppendHtml(row);
                output.PostContent.AppendHtml(footer);
            }
        }
    }
}
