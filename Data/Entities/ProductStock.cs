using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ProductStock : BaseEntity
    {
        public int ProductId { get; set; } // Ürün ID'si
        public Product Product { get; set; } // Ürün ile ilişki

        public int Quantity { get; set; } // Mevcut stok miktarı
        public DateTime LastUpdated { get; set; } // Son güncelleme tarihi
    }
}
