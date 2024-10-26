﻿using MediatR;
using WebApiSample.Models;

namespace WebApiSample.BLL.Products.Commands.Create
{
    public class UpdateProductDescriptionCommand : IRequest
    {
        public int Id { get; set; }
        public string? Description { get; set; }
    }
}
