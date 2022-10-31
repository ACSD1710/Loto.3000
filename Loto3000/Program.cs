using Bogus;
using Loto3000.Application;
using Loto3000.Domain.Models;
using Loto3000.Infrastructure;
using Loto3000.Infrastructure.EntityFrameWork;
using Loto3000.Infrastructure.Repositories;
using Loto3000Application.Exeption;
using Loto3000Application.Repository;
using Loto3000Application.Services;
using Loto3000Application.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Diagnostics;
using System.Text;
using ValidationException = Loto3000Application.Exeption.ValidationException;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddInfrastracture(builder.Configuration);

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


Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(builder.Configuration)
                .CreateLogger();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    try
    {
        var stopwatch = new Stopwatch();
        Log.Logger.Information("Request - ");

        stopwatch.Start();
        await next();
        stopwatch.Stop();
        if (stopwatch.ElapsedMilliseconds > 500)
        {
            Log.Logger.Warning("Request took {duration}", stopwatch.ElapsedMilliseconds);
        }
        Log.Logger.Information("Response");
        
    }
    catch (NotFoundException ex)
    {
        Log.Logger.Warning("An exception occured", ex);
        throw;
    }
    catch (ArgumentLotoExeption ex)
    {
        Log.Logger.Warning("An exception occured", ex);
        throw;
    }
    catch (ValidationException ex)
    {
        Log.Logger.Warning("An exception occured", ex);
        throw;
    }
    catch (Exception ex)
    {
        Log.Logger.Error("An exception occured", ex);
        throw;
    }
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();