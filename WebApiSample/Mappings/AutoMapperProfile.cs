using AutoMapper;
using WebApiSample.Models;

namespace WebApiSample.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDescriptionDto>().ReverseMap();
        }
    }
}
