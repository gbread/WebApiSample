using AutoMapper;
using WebApiSample.BLL.Products.Commands.Create;
using WebApiSample.DAL.DTOs;
using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
            CreateMap<ProductEntity, UpdateProductDescriptionDto>().ReverseMap();
            CreateMap<ProductEntity, CreateProductCommand>().ReverseMap();
            CreateMap<ProductEntity, UpdateProductDescriptionCommand>().ReverseMap();
            CreateMap<IPagedList<Product>, IPagedList<ProductEntity>>()
                       .ConvertUsing((src, dest, context) =>
                       {
                           var mappedItems = context.Mapper.Map<IEnumerable<ProductEntity>>(src);
                           return new StaticPagedList<ProductEntity>(
                               mappedItems,
                               src.PageNumber,
                               src.PageSize,
                               src.TotalItemCount);
                       });
        }
    }
}