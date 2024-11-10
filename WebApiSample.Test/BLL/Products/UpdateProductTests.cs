using AutoMapper;
using Moq;
using WebApiSample.BLL.Products.Commands.Create;
using WebApiSample.DAL;
using WebApiSample.DAL.DTOs;
using WebApiSample.Mappings;

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
            var product = new ProductEntity { Id = 1, Name = "Product 1", Price = 5, Description = "Old Description", ImgUri = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRgzluwUTsgbfDWA1nc8Go8A2nQJvq-U9UlQ&s" };
            _productRepositoryMock
                .Setup(repo => repo.GetProductByIdAsync(It.IsAny<int>(), default))
                .ReturnsAsync(product);

            var updateCommand = new UpdateProductDescriptionCommand { Id = 1, Description = "New Description" };

            // Act
            await _updateProductDescriptionHandler.Handle(updateCommand, default);

            // Assert
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<ProductEntity>(p => p.Description == "New Description"), default), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<ProductEntity>(p => p.Id == 1), default), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<ProductEntity>(p => p.Price == 5), default), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.Is<ProductEntity>(p => p.ImgUri == "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSRgzluwUTsgbfDWA1nc8Go8A2nQJvq-U9UlQ&s"), default), Times.Once);
        }
    }
}