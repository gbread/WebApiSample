using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.DAL
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IPagedList<Product>> GetProductsAsync(int page, int pageSize);
        Task<Product?> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
