﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs.OrderItemDtos
{
    public class OrderDetailAddDto
    {
        public int ProductId { get; set; } // Ürün kimliği
        public int Quantity { get; set; } // Siparişteki ürün miktarı
        
    }
}
