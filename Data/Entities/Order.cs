using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; } 
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
