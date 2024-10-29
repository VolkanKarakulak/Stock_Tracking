using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class StockMovement : BaseEntity
    {      
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // Miktar (pozitif giriş, negatif çıkış)
        public int? Quantity { get; set; }
        public DateTime MovementDate { get; set; } 
        public MovementType MovementType { get; set; } // Giriş ya da çıkış
    }
}
