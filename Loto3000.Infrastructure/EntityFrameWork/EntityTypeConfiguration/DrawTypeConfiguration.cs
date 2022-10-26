using Loto3000.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Loto3000.Infrastructure.EntityFrameWork.EntityTypeConfiguration
{
    public class DrawTypeConfiguration : IEntityTypeConfiguration<Draw>
    {
        public void Configure(EntityTypeBuilder<Draw> builder)
        {
            //builder.HasMany(x => x.Tickets)
            //    .WithOne(x => x.Draw)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
