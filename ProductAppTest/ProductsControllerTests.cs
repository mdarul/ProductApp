using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductApp.Controllers;
using ProductApp.Models;
using ProductApp.Models.DTOs;
using ProductApp.Models.Entities;
using ProductApp.Services;
using Xunit;

namespace ProductAppTest
{
    public class ProductsControllerTests
    {
        private readonly List<Product> mockList;

        public ProductsControllerTests()
        {
            mockList = new List<Product>
            {
                new Product()
                {
                    Id = 1,
                    Name = "test1",
                    Category = "test1",
                    Cost = 1
                },
                new Product()
                {
                    Id = 2,
                    Name = "test2",
                    Category = "test2",
                    Cost = 1
                },
                new Product()
                {
                    Id = 3,
                    Name = "test3",
                    Category = "test3",
                    Cost = 1
                }
            };
        }

        [Fact]
        public void GetAllProductsTest()
        {
            var loggerMock = new Mock<ILogger<ProductsController>>();
            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock
                .Setup(o => o.GetProducts())
                .Returns(mockList);

            var productsController = new ProductsController(productRepositoryMock.Object, loggerMock.Object);
            var getResult = productsController.GetProducts();

            // check response type and map
            var okGetResult = Assert.IsType<OkObjectResult>(getResult);
            // check content type and map
            var okGetContent = Assert.IsAssignableFrom<IEnumerable<ProductDto>>(okGetResult.Value);

            Assert.Equal(mockList.Count, okGetContent.Count());
        }

        [Fact]
        public void GetProductTest()
        {
            var loggerMock = new Mock<ILogger<ProductsController>>();
            var productRepositoryMock = new Mock<IProductRepository>();

            productRepositoryMock
                .Setup(o => o.GetProduct(1))
                .Returns(mockList[1]);

            var productsController = new ProductsController(productRepositoryMock.Object, loggerMock.Object);
            var getResult = productsController.GetProduct(1);

            var okGetResult = Assert.IsType<OkObjectResult>(getResult);
            var okGetContent = Assert.IsType<ProductDto>(okGetResult.Value);

            Assert.Equal(mockList[1].Id, okGetContent.Id);
            Assert.Equal(mockList[1].Name, okGetContent.Name);
            Assert.Equal(mockList[1].Category, okGetContent.Category);
            Assert.Equal(mockList[1].Cost, okGetContent.Cost);
        }

        [Fact]
        public void PostProductTest()
        {
            var loggerMock = new Mock<ILogger<ProductsController>>();
            var productRepositoryMock = new Mock<IProductRepository>();

            var productDto = new ProductForCreationDto()
            {
                Name = "post1",
                Category = "post1",
                Cost = 21
            };
            var newProductsList = new List<Product>(mockList);

            productRepositoryMock
                .Setup(o => o.AddProduct(It.IsAny<Product>()))
                .Callback((Product prod) => newProductsList.Add(prod));

            var productsController = new ProductsController(productRepositoryMock.Object, loggerMock.Object);
            var postResult = productsController.PostProduct(productDto);
            var noContentPostResult = Assert.IsType<NoContentResult>(postResult);

            Assert.Equal(mockList.Count + 1, newProductsList.Count);
            Assert.Contains(newProductsList, o => o.Name == productDto.Name);
            Assert.Contains(newProductsList, o => o.Category == productDto.Category);
            Assert.Contains(newProductsList, o => o.Cost == productDto.Cost);
        }

        [Fact]
        public void PutProductTest()
        {
            var loggerMock = new Mock<ILogger<ProductsController>>();
            var productRepositoryMock = new Mock<IProductRepository>();

            var newProductsList = new List<Product>(mockList);
            var productDto = new ProductForUpdateDto()
            {
                Name = "post1",
                Category = "post1",
                Cost = 31
            };

            productRepositoryMock
                .Setup(o => o.GetProduct(It.IsAny<int>()))
                .Returns((int numArg) => newProductsList.FirstOrDefault(o1 => o1.Id == numArg));

            var productsController = new ProductsController(productRepositoryMock.Object, loggerMock.Object);
            var putResult = productsController.PutProduct(1, productDto);
            var okContentPostResult = Assert.IsType<OkObjectResult>(putResult);

            Assert.Equal(productDto.Name, newProductsList[0].Name);
            Assert.Equal(productDto.Category, newProductsList[0].Category);
            Assert.Equal(productDto.Cost, newProductsList[0].Cost);
        }
    }
}
