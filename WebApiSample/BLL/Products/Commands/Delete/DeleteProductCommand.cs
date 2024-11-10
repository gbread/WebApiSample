using MediatR;

namespace WebApiSample.BLL.Products.Commands.Create
{
    public record DeleteProductCommand(int Id) : IRequest;
}