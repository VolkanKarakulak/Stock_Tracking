using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : BaseEntity
    {
        [StringLength(50), Column(TypeName = "Varchar(50)")]
        public string Fullname { get; set; }

        [StringLength(100), Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [Column(TypeName = "nText"), DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastLoginDate { get; set; }

        [StringLength(20), Column(TypeName = "Varchar(20)")]
        public string LastIPAddress { get; set; }

        public string SaltPassword { get; set; }
        //public UserRefreshToken UserRefreshToken { get; set; }
        public int? RoleId { get; set; }
        //public Role Role { get; set; }
    }
}
