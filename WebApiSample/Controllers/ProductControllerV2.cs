using Microsoft.AspNetCore.Mvc;
using WebApiSample.BLL;
using WebApiSample.Controllers.DTOs;
using WebApiSample.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiSample.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Product")]
    [ApiController]
    public class Product2Controller : Controller
    {
        private readonly IProductServiceV2 _productService;

        public Product2Controller(IProductServiceV2 productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves paginated list of products.
        /// </summary>
        /// <param name="page">The page number (starting at 1).</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paginated list of product DTOs.</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns paginated products", typeof(PagedListDto<ProductDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid pagination parameters")]
        public async Task<ActionResult<PagedListDto<ProductDto>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize must be greater than 0.");
            }

            var products = await _productService.GetProductsAsync(page, pageSize);

            var pagedResult = new PagedListDto<ProductDto>(
                products,
                products.TotalItemCount,
                products.PageNumber,
                products.PageSize
            );

            return Ok(pagedResult);
        }
    }
}
