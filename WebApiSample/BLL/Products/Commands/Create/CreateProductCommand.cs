using MediatR;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string ImgUri { get; set; }
        public string? Description { get; set; }
    }
}
