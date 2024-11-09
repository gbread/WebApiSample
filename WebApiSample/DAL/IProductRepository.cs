using WebApiSample.DAL.DTOs;
using WebApiSample.Models;
using X.PagedList;

namespace WebApiSample.DAL
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetProductsAsync(CancellationToken cancellationToken);

        Task<IPagedList<ProductEntity>> GetProductsAsync(int page, int pageSize, CancellationToken cancellationToken);

        Task<ProductEntity?> GetProductByIdAsync(int id, CancellationToken cancellationToken);

        Task AddProductAsync(ProductEntity product, CancellationToken cancellationToken);

        Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken);

        Task DeleteProductAsync(ProductEntity product, CancellationToken cancellationToken);
    }
}