using Service.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.ProductStockDtos
{
    public class ProductStockUpdateDto
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public bool IsActive { get; set; }
        public int? Quantity { get; set; }


    }
}
