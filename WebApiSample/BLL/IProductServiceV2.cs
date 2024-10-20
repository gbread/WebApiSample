using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.BLL
{
    public interface IProductServiceV2
    {
        Task<IPagedList<ProductDto>> GetProductsAsync(int page, int pageSize);
    }
}
