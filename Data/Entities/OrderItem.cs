﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class OrderItem : BaseEntity
    {      
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        [Range(1, 10000)]
        public int Quantity { get; set; } // Üründen kaç adet alındı

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } // O sırada birim fiyat
    }
}
