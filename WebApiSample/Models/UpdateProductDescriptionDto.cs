namespace WebApiSample.Models
{
    public class UpdateProductDescriptionDto
    {
        public required int Id { get; set; }

        public string? Description { get; set; }
    }
}