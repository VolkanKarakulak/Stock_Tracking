﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ProductStock : BaseEntity
    {     
        public int ProductId { get; set; }
        public Product? Product { get; set; } 
        public int? Quantity { get; set; }
  
    }
}
