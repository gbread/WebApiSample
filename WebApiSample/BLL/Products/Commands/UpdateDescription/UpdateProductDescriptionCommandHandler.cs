using AutoMapper;
using MediatR;
using WebApiSample.BLL.Exceptions;
using WebApiSample.DAL;
using WebApiSample.DAL.Models;

namespace WebApiSample.BLL.Products.Commands.Create
{
    public class UpdateProductDescriptionCommandHandler : IRequestHandler<UpdateProductDescriptionCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductDescriptionCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProductDescriptionCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(command.Id, cancellationToken);
            if (product == null)
            {
                throw new ModelNotFoundException(nameof(Product), command.Id);
            }
            _mapper.Map(command, product);

            await _productRepository.UpdateProductAsync(product, cancellationToken);
        }
    }
}