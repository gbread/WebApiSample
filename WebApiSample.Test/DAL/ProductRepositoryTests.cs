using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApiSample.DAL;
using WebApiSample.DAL.DTOs;
using WebApiSample.Mappings;
using WebApiSample.Models;

namespace WebApiSample.Tests
{
    public class ProductRepositoryTests
    {
        private readonly IMapper _mapper;
        private readonly ProductRepository _productRepository;
        private readonly AppDbContext _context;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new AppDbContext(options);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = config.CreateMapper();

            _productRepository = new ProductRepository(_context, _mapper);
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            await _context.Products.AddRangeAsync(
                new Product { Id = 1, Name = "Product 1", Price = 10, ImgUri = "https://example.com/image1.jpg" },
                new Product { Id = 2, Name = "Product 2", Price = 20, ImgUri = "https://example.com/image2.jpg" }
            );
            await _context.SaveChangesAsync();

            // Act
            var products = await _productRepository.GetProductsAsync(default);

            // Assert
            products.Should().HaveCount(2);
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProduct_WhenImgUriIsValid()
        {
            // Arrange
            var product = new ProductEntity { Id = 3, Name = "Product 3", Price = 30, ImgUri = "https://example.com/image3.jpg" };

            // Act
            await _productRepository.AddProductAsync(product, default);
            var result = await _productRepository.GetProductByIdAsync(3, default);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Product 3");
            result.ImgUri.Should().Be("https://example.com/image3.jpg");
        }

        [Fact]
        public async Task AddProductAsync_ShouldThrowException_WhenImgUriIsMissing()
        {
            // Arrange
            var product = new ProductEntity { Id = 4, Name = "Product 4", Price = 40, ImgUri = null }; // ImgUri is missing

            // Act & Assert
            Func<Task> action = async () => await _productRepository.AddProductAsync(product, default);
            await action.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldRemoveProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = 5, Name = "Product 5", Price = 50, ImgUri = "https://example.com/image5.jpg" };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            //map
            var productEntity = _mapper.Map<ProductEntity>(product);
            // Act
            await _productRepository.DeleteProductAsync(productEntity, default);
            var result = await _productRepository.GetProductByIdAsync(5, default);

            // Assert
            result.Should().BeNull();
        }
    }
}