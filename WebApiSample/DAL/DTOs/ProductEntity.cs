namespace WebApiSample.DAL.DTOs
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImgUri { get; set; }
        public string? Description { get; set; }
    }
}