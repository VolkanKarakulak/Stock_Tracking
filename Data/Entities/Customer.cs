using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Customer : BaseEntity
    {
        [Length(1, 50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = default!;

        [Phone]
        [Length(1, 30)]
        [Column(TypeName = "nvarchar(30)")]
        public string PhoneNumber { get; set; } = default!;

        [EmailAddress]
        [Length(1, 100)]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = default!;
        public ICollection<Order>? Orders { get; set; } 
    }
}
