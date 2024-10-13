using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Id { get; set; }

        // Ürün ile ilişki
        public int ProductId { get; set; }
        public Product Product { get; set; }

        // Sipariş ile ilişki
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Sipariş edilen ürün miktarı
        public int Quantity { get; set; } // Üründen kaç adet alındı
        public decimal UnitPrice { get; set; } // O sırada birim fiyat
    }
}
