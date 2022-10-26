using Bogus;
using Loto3000.Domain.Models;
using Loto3000.Infrastructure;
using Loto3000.Infrastructure.EntityFrameWork;
using Loto3000.Infrastructure.Repositories;
using Loto3000Application.Repository;
using Loto3000Application.Services;
using Loto3000Application.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddScoped<IRepository<Admin>, AdminRepository>();
//builder.Services.AddScoped<IRepository<User>, UserRepository>();
//builder.Services.AddScoped<IRepository<Ticket>, TicketRepository>();
//builder.Services.AddScoped<IRepository<Draw>, DrawRepository>();

builder.Services.AddScoped<IRepository<Admin>, BaseRepository<Admin>>();
builder.Services.AddScoped<IRepository<User>, BaseRepository<User>>();
builder.Services.AddScoped<IRepository<Ticket>, BaseRepository<Ticket>>();
builder.Services.AddScoped<IRepository<Draw>, BaseRepository<Draw>>();
builder.Services.AddScoped<IRepository<Game>, BaseRepository<Game>>();
builder.Services.AddScoped<IRepository<Role>, BaseRepository<Role>>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IDrawService, DrawService>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddScoped<IHashids>((sp) =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var secret = configuration["Dancer"];
    return new Hashids(secret);
});
builder.Services.AddDbContext<ApplicationDbContex>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization Header",
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
            });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(opts =>
//    {
//        opts.Events.OnRedirectToLogin = (contex) =>
//        {
//            contex.Response.StatusCode = StatusCodes.Status401Unauthorized;
//            return Task.CompletedTask;
//        };
//        opts.Events.OnRedirectToAccessDenied = (contex) =>
//        {
//            contex.Response.StatusCode = StatusCodes.Status403Forbidden;
//            return Task.CompletedTask;
//        };
//    });



builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();