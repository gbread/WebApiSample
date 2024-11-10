using WebApiSample.BLL.DTOs;
using WebApiSample.Controllers.DTOs;

namespace WebApiSample.BLL.Products.Queries.PagedProducts
{
    public class GetPagedProductsResponse
    {
        public PagedListDto<ProductDto> PagedProducts { get; set; }
    }
}