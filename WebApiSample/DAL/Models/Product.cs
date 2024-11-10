using System.ComponentModel.DataAnnotations;

namespace WebApiSample.DAL.Models
{
    public class Product
    {
        public required int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        public required decimal Price { get; set; }

        [MaxLength(2048)]
        public required string ImgUri { get; set; }

        [MaxLength(400)]
        public string? Description { get; set; }
    }
}