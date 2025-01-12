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
        [Length(1, 50)]
        [Column(TypeName = "nvarchar(50)")]
        public required string Name { get; set; }
        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
