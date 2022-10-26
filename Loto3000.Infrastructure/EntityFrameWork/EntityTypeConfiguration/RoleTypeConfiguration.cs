using Loto3000.Domain.Models;
using Loto3000Application.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Infrastructure.EntityFrameWork.EntityTypeConfiguration
{
    public class RoleTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role() { Id = 1, Name = SysRoles.Administrator }, new Role() { Id = 2, Name = SysRoles.User });
        }
    }
}
