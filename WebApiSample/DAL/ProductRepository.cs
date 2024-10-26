using Microsoft.EntityFrameworkCore;
using WebApiSample.Models;
using X.PagedList;
using X.PagedList.EF;
using X.PagedList.Extensions;

namespace WebApiSample.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<Product>> GetProductsAsync(int page, int pageSize)
        {
            return await _context.Products.ToPagedListAsync(page, pageSize);
        }

        public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
