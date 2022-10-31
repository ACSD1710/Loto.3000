using Loto3000.Domain.Models;
using Loto3000.Infrastructure.EntityFrameWork;
using Loto3000.Infrastructure.Repositories;
using Loto3000Application.Repository;
using Loto3000Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Infrastructure
{
    public static class Module
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Admin>, BaseRepository<Admin>>();
            services.AddScoped<IRepository<User>, BaseRepository<User>>();
            services.AddScoped<IRepository<Ticket>, BaseRepository<Ticket>>();
            services.AddScoped<IRepository<Draw>, BaseRepository<Draw>>();
            services.AddScoped<IRepository<Game>, BaseRepository<Game>>();
            services.AddScoped<IRepository<Role>, BaseRepository<Role>>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //builder.Services.AddScoped<IRepository<Admin>, AdminRepository>();
            //builder.Services.AddScoped<IRepository<User>, UserRepository>();
            //builder.Services.AddScoped<IRepository<Ticket>, TicketRepository>();
            //builder.Services.AddScoped<IRepository<Draw>, DrawRepository>();



            services.AddDbContext<ApplicationDbContex>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            return services;
        }
    }


     
}
