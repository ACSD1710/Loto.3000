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
                             .HasMaxLength(100)
                             .IsRequired();
            builder.Property(p => p.Password)
                              .HasMaxLength(50)
                              .IsRequired();
            builder.Property(p => p.Username)
                             .HasMaxLength(100)
                             .IsRequired();
            builder.Property(p => p.FirstName)
                            .HasMaxLength(100)
                            .IsRequired();
            builder.Property(p => p.LastName)
                            .HasMaxLength(100)
                            .IsRequired();
           
                            

        }
    }
}
