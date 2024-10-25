using AutoMapper;
using Azure.Core;
using MediatR;
using WebApiSample.DAL;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Queries
{
    public record GetProductQuery(int Id) : IRequest<ProductDto>;

    public class GetProductHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
