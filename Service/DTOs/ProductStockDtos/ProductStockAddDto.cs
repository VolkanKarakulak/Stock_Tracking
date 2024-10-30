using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductStockDtos
{
    public class ProductStockAddDto
    {
       
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

    }
}
