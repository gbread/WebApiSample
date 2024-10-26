using WebApiSample.Controllers.DTOs;
using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.BLL.Products.Queries.PagedProducts
{
    public class GetPagedProductsResponse
    {
        public PagedListDto<ProductDto> PagedProducts { get; set; }
    }
}
