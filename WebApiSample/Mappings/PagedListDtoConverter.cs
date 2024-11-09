using AutoMapper;
using WebApiSample.Controllers.DTOs;
using X.PagedList;

namespace WebApiSample.Mappings
{
    public class PagedListDtoConverter<TSource, TDestination>
        : ITypeConverter<IPagedList<TSource>, PagedListDto<TDestination>>
    {
        private readonly IMapper _mapper;

        public PagedListDtoConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PagedListDto<TDestination> Convert(IPagedList<TSource> source,
                                                  PagedListDto<TDestination> destination,
                                                  ResolutionContext context)
        {
            // Map the items list from TSource to TDestination
            var mappedItems = source.Select(item => _mapper.Map<TDestination>(item)).ToList();

            // Create a new PagedListDto with mapped items and pagination metadata
            return new PagedListDto<TDestination>
            {
                Items = mappedItems,
                PageNumber = source.PageNumber,
                PageSize = source.PageSize,
                TotalCount = source.TotalItemCount
            };
        }
    }
}