using MediatR;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Commands.Create
{
    public record DeleteProductCommand(int Id) : IRequest;

}
