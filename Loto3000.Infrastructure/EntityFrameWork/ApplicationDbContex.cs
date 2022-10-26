using Loto3000.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Loto3000.Infrastructure.EntityFrameWork
{
    public class ApplicationDbContex : DbContext
    {
        public ApplicationDbContex(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);

           modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContex).Assembly);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Draw> Draws { get; set; }
        public DbSet<Ticket>Ticket { get; set; }
        public DbSet<Admin>Admin { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Role> Roles { get;set; } 


    }
}
