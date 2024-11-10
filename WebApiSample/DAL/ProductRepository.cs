using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiSample.DAL.DTOs;
using WebApiSample.DAL.Models;
using X.PagedList;
using X.PagedList.EF;

namespace WebApiSample.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<ProductEntity>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ProductEntity>>(products);
        }

        public async Task<IPagedList<ProductEntity>> GetProductsAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToPagedListAsync(page, pageSize, null, cancellationToken);

            return _mapper.Map<IPagedList<ProductEntity>>(products);
        }

        public async Task<ProductEntity?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return _mapper.Map<ProductEntity?>(product);
        }

        public async Task AddProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            var productDb = _mapper.Map<Product>(product);
            await _context.Products.AddAsync(productDb, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            var productDb = _mapper.Map<Product>(product);
            _context.Entry(productDb).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(ProductEntity product, CancellationToken cancellationToken)
        {
            var productDb = _mapper.Map<Product>(product);
            _context.Products.Remove(productDb);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}