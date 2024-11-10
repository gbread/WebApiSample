using AutoMapper;
using MediatR;
using WebApiSample.DAL;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>> { }

    public class GetProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsAsync(cancellationToken);
            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}