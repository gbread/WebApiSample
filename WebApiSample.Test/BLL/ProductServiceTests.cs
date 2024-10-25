using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSample.BLL;
using WebApiSample.DAL;
using WebApiSample.Mappings;
using WebApiSample.Models;

namespace WebApiSample.Test.BLL
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductServiceTests()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>(); // Assuming this is your mapping profile
            });
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnListOfProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10, Description = "Description 1", ImgUri = "https://media.gettyimages.com/id/1327018451/photo/washington-dc-international-film-and-television-star-and-the-worlds-most-famous-amphibian.jpg?s=612x612&w=gi&k=20&c=PWkRIp_5HcpQ4WetKzlybMTvdfKyIfsD6HL3GF4S3dI=" },
                new Product { Id = 2, Name = "Product 2", Price = 20, Description = "Description 2", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"}
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(default))
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetProductsAsync(default);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(p => p.Name == "Product 1");
        }

        [Fact]
        public async Task UpdateProductDescription_ShouldUpdateDescription_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 5, Description = "Old Description", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRgzluwUTsgbfDWA1nc8Go8A2nQJvq-U9UlQ&s" };
            _productRepositoryMock
                .Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(product);

            var updateDto = new UpdateProductDescriptionDto { Id = 1 ,Description = "New Description" };

            // Act
            await _productService.UpdateProductDescriptionAsync(updateDto);


            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Description == "New Description")), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Id == 1)), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.Price == 5)), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<Product>(p => p.ImgUri == "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRgzluwUTsgbfDWA1nc8Go8A2nQJvq-U9UlQ&s")), Times.Once);
        }
    }

}
