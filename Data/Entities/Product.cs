using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Product : BaseEntity
    {
        [Length(1, 250)]
        [Column(TypeName = "nvarchar(250)")]
        public required string Name { get; set; }
        public int? Stock { get; set; }

        [Column(TypeName = "decimal(15,2)")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        [Length(1, 30)]
        [Column(TypeName = "nvarchar(30)")]
        public string? Color { get; set; }

        [Length(1, 50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? Material { get; set; }
        public ProductStock? ProductStock { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<ProductCategory>? ProductCategories { get; set; }


    }
}
