using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiSample.BLL.Products.Commands.Create;
using WebApiSample.BLL.Products.Queries;
using WebApiSample.Models;

namespace WebApiSample.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
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
            return await _mediator.Send(new GetAllProductsQuery(), HttpContext.RequestAborted);
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
            return await _mediator.Send(new GetProductQuery(id));
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">The product DTO to create.</param>
        /// <returns>The created product DTO.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "Product successfully created", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
        public async Task<ActionResult<CreateProductResponse>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            return await _mediator.Send(createProductCommand);
        }

        /// <summary>
        /// Updates a product's description.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="productDescriptionDto">The updated product description DTO.</param>
        /// <returns>No content if update is successful.</returns>
        [HttpPatch()]
        [SwaggerResponse(StatusCodes.Status204NoContent, "Product description successfully updated")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input or product ID mismatch")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
        public async Task<IActionResult> UpdateProductDescription([FromBody] UpdateProductDescriptionCommand updateProductDescriptionCommand)
        {
            await _mediator.Send(updateProductDescriptionCommand);
            return NoContent();
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
            await _mediator.Send(new DeleteProductCommand(id));
            return NoContent();
        }
    }
}