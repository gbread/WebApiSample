using AutoMapper;
using FluentAssertions;
using Moq;
using WebApiSample.BLL.Products.Queries;
using WebApiSample.DAL;
using WebApiSample.DAL.DTOs;
using WebApiSample.Mappings;

namespace WebApiSample.Test.BLL.Products
{
    public class GetAllProductsTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetProductsQueryHandler _getProductsHandler;

        public GetAllProductsTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _getProductsHandler = new GetProductsQueryHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnListOfProducts_WhenProductsDoesntExist()
        {
            // Arrange
            var products = new List<ProductEntity>
            {
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(default))
                .ReturnsAsync(products);

            // Act
            var result = await _getProductsHandler.Handle(new GetAllProductsQuery(), default);

            // Assert
            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturnListOfProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<ProductEntity>
            {
                new ProductEntity { Id = 1, Name = "Product 1", Price = 10, Description = "Description 1", ImgUri = "https://media.gettyimages.com/id/1327018451/photo/washington-dc-international-film-and-television-star-and-the-worlds-most-famous-amphibian.jpg?s=612x612&w=gi&k=20&c=PWkRIp_5HcpQ4WetKzlybMTvdfKyIfsD6HL3GF4S3dI=" },
                new ProductEntity { Id = 2, Name = "Product 2", Price = 20, Description = "Description 2", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"}
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(default))
                .ReturnsAsync(products);

            // Act
            var result = await _getProductsHandler.Handle(new GetAllProductsQuery(), default);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(p => p.Name == "Product 1");
            result.Should().Contain(p => p.Name == "Product 2");
        }
    }
}