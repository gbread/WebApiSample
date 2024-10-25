using Microsoft.AspNetCore.Mvc;
using WebApiSample.BLL;
using WebApiSample.Models;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using WebApiSample.BLL.Products.Queries;

namespace WebApiSample.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IMediator mediator)
        {
            _productService = productService;
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of product DTOs.</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all products", typeof(IEnumerable<ProductDto>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery(), HttpContext.RequestAborted);
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product DTO.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns the product", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _mediator.Send(new GetProductQuery(id));
            if (product == null) return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product DTO to create.</param>
        /// <returns>The created product DTO.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Product successfully created", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _productService.AddProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, productDto);
        }

        /// <summary>
        /// Updates a product's description.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDescriptionDto">The updated product description DTO.</param>
        /// <returns>No content if update is successful.</returns>
        [HttpPut("{id}/description")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Product description successfully updated")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input or product ID mismatch")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> UpdateProductDescription(int id, [FromBody] UpdateProductDescriptionDto productDescriptionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != productDescriptionDto.Id) return BadRequest("Product ID mismatch");
            try
            {
                await _productService.UpdateProductDescriptionAsync(productDescriptionDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content if deletion is successful.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Product successfully deleted")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
