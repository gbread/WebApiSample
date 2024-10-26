using AutoMapper;
using WebApiSample.BLL.Products.Commands.Create;
using WebApiSample.Models;

namespace WebApiSample.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDescriptionDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
        }
    }
}
