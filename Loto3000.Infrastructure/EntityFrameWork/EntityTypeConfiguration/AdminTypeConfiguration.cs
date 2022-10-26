using Loto3000.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Infrastructure.EntityFrameWork.EntityTypeConfiguration
{
    public class AdminTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            

          //  builder.HasOne(b => b.Game)
          //       .WithOne(i => i.Admin)
          //       .HasForeignKey<Admin>(b => b.GameForeignKey);
          // builder.HasOne(b => b.Draw)
          //.WithOne(i => i.Admin)
          //.HasForeignKey<Admin>(b => b.DrawForeignKey);


        }
    }
}
