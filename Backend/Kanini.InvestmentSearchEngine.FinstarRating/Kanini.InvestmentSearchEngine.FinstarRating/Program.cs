using Kanini.InvestmentSearchEngine.FinstarRating.Interfaces;
using Kanini.InvestmentSearchEngine.FinstarRating.Models;
using Kanini.InvestmentSearchEngine.FinstarRating.Repositories;
using Kanini.InvestmentSearchEngine.FinstarRating.Services;
using Kanini.InvestmentSearchEngine.FinstarRating.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Kanini.InvestmentSearchEngine.FinstarRating.Mappers;

namespace Kanini.InvestmentSearchEngine.FinstarRating
{
    [ExcludeFromCodeCoverage]

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<FinstarContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("MyConn"));
            });

            #region Dependency Injection
            builder.Services.AddScoped<IRepo<int, Finstar>, FinstarRepository>();
            builder.Services.AddScoped<IFinstarService, FinstarService>();
            builder.Services.AddScoped<IMapper, Mapper>();
            #endregion
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("CORS", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("CORS");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}