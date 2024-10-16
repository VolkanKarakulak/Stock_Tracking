using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }

        [Length(1, 200)]
        [Column(TypeName = "nvarchar(200)")]

        public ICollection<Product>? Products { get; set; }
    }
}
