﻿namespace WebApiSample.BLL.DTOs
{
    public class ProductDto
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string ImgUri { get; set; }
        public string? Description { get; set; }
    }
}