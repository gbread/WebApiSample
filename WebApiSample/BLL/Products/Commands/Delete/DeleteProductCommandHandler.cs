using AutoMapper;
using MediatR;
using WebApiSample.BLL.Exceptions;
using WebApiSample.BLL.Products.Queries;
using WebApiSample.DAL;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Commands.Create
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(command.Id, cancellationToken);
            if (product == null)
            {
                throw new ModelNotFoundException(nameof(Product), command.Id);
            }

            await _productRepository.DeleteProductAsync(product, cancellationToken);
        }
    }
}
