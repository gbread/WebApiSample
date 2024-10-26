using AutoMapper;
using WebApiSample.DAL;
using WebApiSample.Models;

namespace WebApiSample.BLL
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task AddProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddProductAsync(product, default);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id, default);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }
            await _productRepository.DeleteProductAsync(product, default);
        }

        public async Task UpdateProductDescriptionAsync(UpdateProductDescriptionDto productDescriptionDto)
        {
            var product = await _productRepository.GetProductByIdAsync(productDescriptionDto.Id, default);
            if (product == null)
            {
                throw new KeyNotFoundException();
            }
            _mapper.Map(productDescriptionDto, product);

            await _productRepository.UpdateProductAsync(product);
        }
    }
}
