using Microsoft.AspNetCore.Mvc;
using WebApiSample.BLL;
using WebApiSample.Controllers.DTOs;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    [Route("api/v2/Product")]
    [ApiController]
    public class ProductControllerV2 : Controller
    {
        private readonly IProductServiceV2 _productService;

        public ProductControllerV2(IProductServiceV2 productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedListDto<ProductDto>>> GetProducts(int page, int pageSize)
        {
            var products = await _productService.GetProductsAsync(page, pageSize);

            return Ok(
                new PagedListDto<ProductDto>(
                    products, 
                    products.TotalItemCount, 
                    products.PageNumber, 
                    products.PageSize)
            );
        }
    }
}
