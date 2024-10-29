using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        [StringLength(50), Column(TypeName = "Varchar(50)")]
        public string FirstName { get; set; } = default!;

        [StringLength(75), Column(TypeName = "Varchar(75)")]
        public string LastName { get; set; } = default!;
        public string FullName => string.Join(" ", FirstName, LastName);

        [StringLength(100), Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = default!;

        [Column(TypeName = "nText"), DataType(DataType.Password)]
        public required string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastLoginDate { get; set; }

        [StringLength(20), Column(TypeName = "Varchar(20)")]
        public string LastIPAddress { get; set; } = default!;

        public string SaltPassword { get; set; }
        //public UserRefreshToken UserRefreshToken { get; set; }
        public int? RoleId { get; set; }
        //public Role Role { get; set; }
    }
}
