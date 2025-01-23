using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Moq;

using SportsStore.Infrastructure;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class PageLinkTagHelperTests
    {
        [Fact]
        public void Can_Generate_Page_Links()
        {
            // Arrange
            const string PAGE_ONE_STRING = "Test/Page1";
            const string PAGE_TWO_STRING = "Test/Page2";
            const string PAGE_THREE_STRING = "Test/Page3";
            Mock<IUrlHelper> urlHelper = new();
            urlHelper.SetupSequence(x =>
                x.Action(It.IsAny<UrlActionContext>()))
                    .Returns(PAGE_ONE_STRING)
                    .Returns(PAGE_TWO_STRING)
                    .Returns(PAGE_THREE_STRING);

            Mock<IUrlHelperFactory> urlHelperFactory = new();
            urlHelperFactory.Setup(f =>
                f.GetUrlHelper(It.IsAny<ActionContext>()))
                    .Returns(urlHelper.Object);

            Mock<ViewContext> viewContext = new();

            PageLinkTagHelper helper = new(urlHelperFactory.Object)
            {
                PageModel = new()
                {
                    TotalItems = 28,
                    ItemsPerPage = 10,
                    CurrentPage = 2
                },
                ViewContext = viewContext.Object,
                PageAction = "Test"
            };

            TagHelperContext ctx = new(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                string.Empty);

            Mock<TagHelperContent> content = new();
            TagHelperOutput output = new(
                "div",
                new TagHelperAttributeList(),
                (cache, encoder) =>
                    Task.FromResult(content.Object));

            // Act
            helper.Process(ctx, output);

            // Assert
            Assert.Equal(
                $@"<a href=""{PAGE_ONE_STRING}"">1</a>" +
                $@"<a href=""{PAGE_TWO_STRING}"">2</a>" +
                $@"<a href=""{PAGE_THREE_STRING}"">3</a>",
                output.Content.GetContent());
        }
    }
}
