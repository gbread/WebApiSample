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
using X.PagedList;

namespace WebApiSample.Test.BLL
{
    public class ProductServiceV2Tests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IProductServiceV2 _productService;
        private readonly IMapper _mapper;

        public ProductServiceV2Tests()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>(); // Assuming this is your mapping profile
            });
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductServiceV2(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnListOfProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10, Description = "Description 1", ImgUri = "https://media.gettyimages.com/id/1327018451/photo/washington-dc-international-film-and-television-star-and-the-worlds-most-famous-amphibian.jpg?s=612x612&w=gi&k=20&c=PWkRIp_5HcpQ4WetKzlybMTvdfKyIfsD6HL3GF4S3dI=" },
                new Product { Id = 2, Name = "Product 2", Price = 20, Description = "Description 2", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"},
                new Product { Id = 3, Name = "Product 3", Price = 30, Description = "Description 2", ImgUri = "https://pic.cz"}
            };

            var pagedList = new PagedList<Product>(products, 2, 2);

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(It.IsAny<int>(), It.IsAny<int>(), default))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _productService.GetProductsAsync(2, 2);

            // Assert
            result.Should().HaveCount(1);
            result.First().Should().BeEquivalentTo(new ProductDto { Id = 3, Name = "Product 3", Price = 30, Description = "Description 2", ImgUri = "https://pic.cz" });
        }

        
    }

}
