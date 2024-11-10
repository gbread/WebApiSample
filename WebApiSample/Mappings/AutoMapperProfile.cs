using AutoMapper;
using WebApiSample.BLL.DTOs;
using WebApiSample.BLL.Products.Commands.Create;
using WebApiSample.BLL.Products.Queries.PagedProducts;
using WebApiSample.Controllers.DTOs;
using WebApiSample.DAL.DTOs;
using WebApiSample.DAL.Models;
using X.PagedList;

namespace WebApiSample.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<ProductEntity, CreateProductCommand>().ReverseMap();
            CreateMap<ProductEntity, UpdateProductDescriptionCommand>().ReverseMap();

            CreateMap<IPagedList<ProductDto>, GetPagedProductsResponse>()
                .ForMember(dest => dest.PagedProducts, opt => opt.MapFrom(src => src));

            CreateMap(typeof(IPagedList<>), typeof(IPagedList<>))
                .ConvertUsing(typeof(PagedListConverter<,>));

            CreateMap(typeof(IPagedList<>), typeof(PagedListDto<>))
                .ConvertUsing(typeof(PagedListDtoConverter<,>));
        }
    }
}