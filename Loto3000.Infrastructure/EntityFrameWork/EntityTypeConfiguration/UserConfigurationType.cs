using Loto3000.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFrameWork.EntityTypeConfiguration
{
    public class UserConfigurationType : IEntityTypeConfiguration<User> 
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Email)
                .HasMaxLength(512)
                .IsRequired();
            builder.Property(p => p.Password)
                .HasMaxLength(1000)
                .IsRequired();
           
        }
    }
}
