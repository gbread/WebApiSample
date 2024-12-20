﻿using AutoMapper;
using MediatR;
using WebApiSample.BLL.DTOs;
using WebApiSample.BLL.Exceptions;
using WebApiSample.DAL;
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

            var productDtos = _mapper.Map<IPagedList<ProductDto>>(products);

            var response = _mapper.Map<GetPagedProductsResponse>(productDtos);

            return response;
        }
    }
}