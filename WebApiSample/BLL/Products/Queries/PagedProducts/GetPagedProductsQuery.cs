using MediatR;

namespace WebApiSample.BLL.Products.Queries.PagedProducts
{
    public class GetPagedProductsQuery : IRequest<GetPagedProductsResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
