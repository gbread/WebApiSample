using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.DAL
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
        Task<IPagedList<Product>> GetProductsAsync(int page, int pageSize);
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
