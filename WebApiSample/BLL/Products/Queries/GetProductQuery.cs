using AutoMapper;
using MediatR;
using WebApiSample.BLL.Exceptions;
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

            if (product == null)
            {
                throw new ModelNotFoundException(nameof(product), request.Id);
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }
    }
}