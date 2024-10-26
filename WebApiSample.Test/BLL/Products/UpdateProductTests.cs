using AutoMapper;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSample.BLL.Products.Commands.Create;
using WebApiSample.BLL.Products.Queries;
using WebApiSample.DAL;
using WebApiSample.Mappings;
using WebApiSample.Models;

namespace WebApiSample.Test.BLL.Products
{
    public class UpdateProductTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly UpdateProductDescriptionCommandHandler _updateProductDescriptionHandler;

        public UpdateProductTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _updateProductDescriptionHandler = new UpdateProductDescriptionCommandHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task UpdateProductDescription_ShouldUpdateDescription_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 5, Description = "Old Description", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRgzluwUTsgbfDWA1nc8Go8A2nQJvq-U9UlQ&s" };
            _productRepositoryMock
                .Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>(), default))
                .ReturnsAsync(product);

            var updateCommand = new UpdateProductDescriptionCommand { Id = 1, Description = "New Description" };

            // Act
            await _updateProductDescriptionHandler.Handle(updateCommand, default);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Description == "New Description")), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Id == 1)), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Price == 5)), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.ImgUri == "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRgzluwUTsgbfDWA1nc8Go8A2nQJvq-U9UlQ&s")), Times.Once);
        }
    }
}