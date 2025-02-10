using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole> 
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
        builder
            .HasKey(ps => new { ps.UserId, ps.RoleId });

        builder
            .HasOne(ps => ps.User)
            .WithMany(s => s.UserRoles)
            .HasForeignKey(ps => ps.UserId);

        builder
            .HasOne(ps => ps.Role)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(ps => ps.RoleId);

        }
    }
}



