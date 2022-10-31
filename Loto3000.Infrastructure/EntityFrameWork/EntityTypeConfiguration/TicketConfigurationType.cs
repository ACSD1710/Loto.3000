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
    public class TicketConfigurationType : IEntityTypeConfiguration<Draw> 
    {
        public void Configure(EntityTypeBuilder<Draw> builder)
        {
         builder.HasOne(x => x.Admin)
                .WithMany(p => p.Draw)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientNoAction);
         
               

        }
    }
}
