using AutoMapper;
using X.PagedList;

namespace WebApiSample.Mappings
{
    public class PagedListConverter<TSource, TDestination>
    : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
    {
        private readonly IMapper _mapper;

        public PagedListConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IPagedList<TDestination> Convert(IPagedList<TSource> source,
                                                IPagedList<TDestination> destination,
                                                ResolutionContext context)
        {
            // Map the items list from TSource to TDestination
            var mappedItems = source.Select(item => _mapper.Map<TDestination>(item)).ToList();

            // Create a new PagedList with mapped items and pagination metadata
            return new StaticPagedList<TDestination>(mappedItems, source.PageNumber, source.PageSize, source.TotalItemCount);
        }
    }
}