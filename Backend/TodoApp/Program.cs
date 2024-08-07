using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using TodoApp.Data;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Repositories;
using TodoApp.Infrastructure;
using TodoApp.Service.Helper;
using TodoApp.Service.Interfaces;
using TodoApp.Service.services;

namespace TodoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TodoAppDb>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoAppDB"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidIssuer = builder.Configuration["Jwt:Issuer"],
                   ValidAudience = builder.Configuration["Jwt:Audience"],
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
               };
           });

            builder.Services.AddAuthorization();

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<TodoAppDb>()
            .AddDefaultTokenProviders();

            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<RequestContextBuilder>();
            builder.Services.AddScoped(opt =>
            {
                var requestContext = opt.GetRequiredService<RequestContextBuilder>();
                return requestContext.Build();
            });

            Log.Logger = new LoggerConfiguration()
           .WriteTo.File("Logs/Log-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(_ => { });
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
