using AutoMapper;
using MediatR;
using WebApiSample.BLL.Exceptions;
using WebApiSample.BLL.Products.Queries;
using WebApiSample.DAL;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Commands.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _productRepository.AddProductAsync(product, cancellationToken);
            var newProduct = _mapper.Map<ProductDto>(product);
            
            var result = new CreateProductResponse
            {
                productDto = newProduct,
            };

            return result;
        }
    }
}
