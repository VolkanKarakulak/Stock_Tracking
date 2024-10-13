using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } // Sipariş tarihi

        // Müşteri ile ilişki
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Siparişin ürünleri
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
