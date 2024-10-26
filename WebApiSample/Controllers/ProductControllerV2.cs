using Microsoft.AspNetCore.Mvc;
using WebApiSample.BLL;
using WebApiSample.Controllers.DTOs;
using WebApiSample.Models;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using WebApiSample.BLL.Products.Queries.PagedProducts;

namespace WebApiSample.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/Product")]
    [ApiController]
    public class Product2Controller : Controller
    {
        private readonly IMediator _mediator;

        public Product2Controller(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<ActionResult<GetPagedProductsResponse>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pagedResultQuery = new GetPagedProductsQuery { Page = page, PageSize = pageSize };

            return await _mediator.Send(pagedResultQuery, HttpContext.RequestAborted);
        }
    }
}