using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.DAL
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
        Task<IPagedList<Product>> GetProductsAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(Product product, CancellationToken cancellationToken);
    }
}
