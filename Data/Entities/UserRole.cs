using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Kullanıcıya rolün atandığı tarih
        public bool IsActive { get; set; } = true; // Rol aktif mi?
    }
}
