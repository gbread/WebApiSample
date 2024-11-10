using WebApiSample.Controllers.DTOs;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Queries.PagedProducts
{
    public class GetPagedProductsResponse
    {
        public PagedListDto<ProductDto> PagedProducts { get; set; }
    }
}