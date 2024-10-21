using AutoMapper;
using WebApiSample.DAL;
using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.BLL
{
    public class ProductServiceV2 : IProductServiceV2
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductServiceV2(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IPagedList<ProductDto>> GetProductsAsync(int page, int pageSize)
        {
            var products = await _productRepository.GetProductsAsync(page, pageSize);

            var productDtos = products
                .Select(product => _mapper.Map<ProductDto>(product))
                .ToList();

            var pagedResult = new StaticPagedList<ProductDto>(
               productDtos,
               products.PageNumber,
               products.PageSize,
               products.TotalItemCount);

            return pagedResult;
        }
    }
}
