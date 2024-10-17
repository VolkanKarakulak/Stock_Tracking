﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductDtos
{
    public class ProductDto : BaseDto
    {
        public required string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
        public string? Description { get; set; } 

    }
}
