using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSample.BLL.Exceptions;
using WebApiSample.BLL.Products.Queries;
using WebApiSample.BLL.Products.Queries.PagedProducts;
using WebApiSample.DAL;
using WebApiSample.Mappings;
using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.Test.BLL.Products
{
    public class GetPagedProductsTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly GetPagedQueryHandler _getProductsHandler;

        public GetPagedProductsTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();

            _productRepositoryMock = new Mock<IProductRepository>();
            _getProductsHandler = new GetPagedQueryHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetPagedProducts_ShouldReturnListOfProducts_WhenProductsDoesntExist()
        {
            // Arrange
            var products = new List<Product>
            {
            };

            var pagedList = new PagedList<Product>(products, 1, 2);

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(It.IsAny<int>(), It.IsAny<int>(), default))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _getProductsHandler.Handle(new GetPagedProductsQuery { Page = 1, PageSize = 2 }, default);

            // Assert
            result.PagedProducts.Items.Should().HaveCount(0);
            result.PagedProducts.PageNumber.Should().Be(1);
            result.PagedProducts.PageSize.Should().Be(2);
            result.PagedProducts.TotalCount.Should().Be(0);
        }

        [Fact]
        public async Task GetPagedProducts_ShouldReturnFirstPage_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10, Description = "Description 1", ImgUri = "https://media.gettyimages.com/id/1327018451/photo/washington-dc-international-film-and-television-star-and-the-worlds-most-famous-amphibian.jpg?s=612x612&w=gi&k=20&c=PWkRIp_5HcpQ4WetKzlybMTvdfKyIfsD6HL3GF4S3dI=" },
                new Product { Id = 2, Name = "Product 2", Price = 20, Description = "Description 2", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"},
                new Product { Id = 3, Name = "Product 3", Price = 30, Description = "Description 3", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"},
                new Product { Id = 4, Name = "Product 4", Price = 40, Description = "Description 4", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"},
            };

            var pagedList = new PagedList<Product>(products, 1, 2);

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(It.Is<int>(x => x == 1), It.Is<int>(x => x == 2), default))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _getProductsHandler.Handle(new GetPagedProductsQuery { Page = 1, PageSize = 2 }, default);

            // Assert
            result.PagedProducts.Items.Should().HaveCount(2);
            result.PagedProducts.Items.Should().Contain(p => p.Name == "Product 1");
            result.PagedProducts.Items.Should().Contain(p => p.Name == "Product 2");
        }

        [Fact]
        public async Task GetPagedProducts_ShouldReturnSecondPage_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10, Description = "Description 1", ImgUri = "https://media.gettyimages.com/id/1327018451/photo/washington-dc-international-film-and-television-star-and-the-worlds-most-famous-amphibian.jpg?s=612x612&w=gi&k=20&c=PWkRIp_5HcpQ4WetKzlybMTvdfKyIfsD6HL3GF4S3dI=" },
                new Product { Id = 2, Name = "Product 2", Price = 20, Description = "Description 2", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"},
                new Product { Id = 3, Name = "Product 3", Price = 30, Description = "Description 3", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"},
                new Product { Id = 4, Name = "Product 4", Price = 40, Description = "Description 4", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRALVqKDzVCPZ3mfStfyt6Ijexu3BzP8KWLeA&s"},
            };

            var pagedList = new PagedList<Product>(products, 2, 2);

            _productRepositoryMock
                .Setup(repo => repo.GetProductsAsync(It.Is<int>(x => x == 2), It.Is<int>(x => x == 2), default))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _getProductsHandler.Handle(new GetPagedProductsQuery { Page = 2, PageSize = 2 }, default);

            // Assert
            result.PagedProducts.Items.Should().HaveCount(2);
            result.PagedProducts.Items.Should().Contain(p => p.Name == "Product 3");
            result.PagedProducts.Items.Should().Contain(p => p.Name == "Product 4");
        }

        [Fact]
        public async Task GetPagedProducts_ShouldThrowException_WhenZeroPage()
        {
            // Act
            var act = async () => await _getProductsHandler.Handle(new GetPagedProductsQuery { Page = 0, PageSize = 2 }, default);

            // Assert
            await act.Should().ThrowExactlyAsync<ModelValidationException>();
        }

        [Fact]
        public async Task GetPagedProducts_ShouldThrowException_WhenNegativePage()
        {
            // Act
            var act = async () => await _getProductsHandler.Handle(new GetPagedProductsQuery { Page = -3, PageSize = 2 }, default);

            // Assert
            await act.Should().ThrowExactlyAsync<ModelValidationException>();
        }

        [Fact]
        public async Task GetPagedProducts_ShouldThrowException_WhenZeroPageSize()
        {
            // Act
            var act = async () => await _getProductsHandler.Handle(new GetPagedProductsQuery { Page = 1, PageSize = 0 }, default);

            // Assert
            await act.Should().ThrowExactlyAsync<ModelValidationException>();
        }

        [Fact]
        public async Task GetPagedProducts_ShouldThrowException_WhenNegativePageSize()
        {
            // Act
            var act = async () => await _getProductsHandler.Handle(new GetPagedProductsQuery { Page = 1, PageSize = -2 }, default);

            // Assert
            await act.Should().ThrowExactlyAsync<ModelValidationException>();
        }
    }
}