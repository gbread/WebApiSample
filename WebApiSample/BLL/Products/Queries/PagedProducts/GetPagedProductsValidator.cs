using FluentValidation;

namespace WebApiSample.BLL.Products.Queries.PagedProducts
{
    public class GetPagedProductsValidator : AbstractValidator<GetPagedProductsQuery>
    {
        public GetPagedProductsValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page needs to be greater than 0");

            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize needs to be greater than 0");
        }
    }
}