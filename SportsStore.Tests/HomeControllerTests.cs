﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SportsStore.Controllers;
using SportsStore.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        private void Can_Use_Repo()
        {
            // Arrange
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" }
            }).AsQueryable());

            HomeController controller = new(mock.Object);

            // Act
            IEnumerable<Product>? result =
                (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product> ??
                    Enumerable.Empty<Product>();

            // Assert
            Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" }
            }).AsQueryable());

            HomeController controller = new(mock.Object);
            controller.PageSize = 3;

            // Act
            IEnumerable<Product> result =
                (controller.Index(2) as ViewResult)?.ViewData.Model as IEnumerable<Product> ??
                    Enumerable.Empty<Product>();

            // Assert
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }
    }
}
