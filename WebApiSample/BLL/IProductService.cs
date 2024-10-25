using WebApiSample.Models;

namespace WebApiSample.BLL
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductDto productDto);
        Task UpdateProductAsync(ProductDto productDto);
        Task DeleteProductAsync(int id);
        Task UpdateProductDescriptionAsync(UpdateProductDescriptionDto productDescriptionDto);
    }
}
