using Microsoft.AspNetCore.Http;

using Moq;

using SportsStore.Models;
using SportsStore.Pages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Tests
{
    public class CartPageTests
    {
        [Fact]
        public void Can_Load_Cart()
        {
            // Arrange
            // create mock repo
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Mock<IStoreRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.Products)
                .Returns((new Product[] { p1, p2 })
                .AsQueryable());

            // create a cart
            Cart testCart = new();
            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            // create mock page context and session
            Mock<ISession> mockSession = new();
            byte[]? data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(testCart));
            mockSession.Setup(session => session.TryGetValue(It.IsAny<string>(), out data));
            Mock<HttpContext> mockContext = new();
            mockContext.SetupGet(context => context.Session).Returns(mockSession.Object);

            // Action
            CartModel cartModel = new(mockRepo.Object)
            {
                PageContext = new PageContext(new ActionContext
                {
                    HttpContext = mockContext.Object,
                    RouteData = new(),
                    ActionDescriptor = new PageActionDescriptor()
                })
            };
            cartModel.OnGet("myUrl");

            // Assert
            Assert.Equal(2, cartModel.Cart?.Lines.Count);
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        [Fact]
        public void Can_Update_Cart()
        {
            // Arrange
            // create mock repo
            Mock<IStoreRepository> mockRepo = new();
            mockRepo.Setup(repo => repo.Products)
                .Returns((new Product[] { new Product { ProductID = 1, Name = "P1" } })
                .AsQueryable());

            // create a cart
            Cart? testCart = new();

            // create mock page context and session
            Mock<ISession> mockSession = new();
            mockSession.Setup(session => session.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                .Callback<string,byte[]>((key, val) =>
                    testCart = JsonSerializer.Deserialize<Cart>(Encoding.UTF8.GetString(val)));

            Mock<HttpContext> mockContext = new();
            mockContext.SetupGet(context => context.Session).Returns(mockSession.Object);

            // Action
            CartModel cartModel = new(mockRepo.Object)
            {
                PageContext = new PageContext(new ActionContext
                {
                    HttpContext = mockContext.Object,
                    RouteData = new(),
                    ActionDescriptor = new PageActionDescriptor()
                })
            };
            cartModel.OnPost(1, "myUrl");

            // Assert
            Assert.Single(cartModel.Cart.Lines);
            Assert.Equal("P1", cartModel.Cart.Lines.First().Product.Name);
            Assert.Equal(1, cartModel.Cart.Lines.First().Quantity);
        }
    }
}
