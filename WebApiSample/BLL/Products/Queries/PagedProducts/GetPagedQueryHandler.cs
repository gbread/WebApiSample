using AutoMapper;
using MediatR;
using WebApiSample.BLL.Exceptions;
using WebApiSample.BLL.Products.Commands.Create;
using WebApiSample.DAL;
using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.BLL.Products.Queries.PagedProducts
{
    public class GetPagedQueryHandler : IRequestHandler<GetPagedProductsQuery, GetPagedProductsResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetPagedQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<GetPagedProductsResponse> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var getPagedProductsValidator = new GetPagedProductsValidator();
            var validationResult = await getPagedProductsValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ModelValidationException(validationResult);
            }

            var products = await _productRepository.GetProductsAsync(request.Page, request.PageSize, cancellationToken);

            var productDtos = products
                .Select(product => _mapper.Map<ProductDto>(product))
                .ToList();

            return new GetPagedProductsResponse
            {
                PagedProducts = new Controllers.DTOs.PagedListDto<ProductDto> { 
                    Items = productDtos,
                    PageNumber = products.PageNumber,
                    PageSize = products.PageSize,
                    TotalCount = products.TotalItemCount
                }
            };
        }
    }
}
