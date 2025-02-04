using Microsoft.AspNetCore.Mvc;

using Moq;

using SportsStore.Controllers;
using SportsStore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            Order order = new();
            OrderController target = new(mock.Object, cart);

            // Act
            ViewResult? result = target.Checkout(order) as ViewResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never());
            Assert.True(string.IsNullOrWhiteSpace(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            cart.AddItem(new(), 1);
            OrderController target = new(mock.Object, cart);
            target.ModelState.AddModelError("error", "Please enter a city name");

            // Act
            ViewResult? result = target.Checkout(new()) as ViewResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never());
            Assert.True(string.IsNullOrWhiteSpace(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_Valid_ShippingDetails()
        {
            // Arrange
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            cart.AddItem(new(), 1);
            OrderController target = new(mock.Object, cart);

            // Act
            RedirectToPageResult? result = target.Checkout(new Order()) as RedirectToPageResult;

            // Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once());
            Assert.Equal("/Completed", result?.PageName);
        }
    }
}
