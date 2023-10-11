using Kanini.InvestmentSearchEngine.LoginWatchList.Context;
using Kanini.InvestmentSearchEngine.LoginWatchList.Interfaces;
using Kanini.InvestmentSearchEngine.LoginWatchList.Repository;
using Kanini.InvestmentSearchEngine.LoginWatchList.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Kanini.InvestmentSearchEngine.LoginWatchList.Services;
using Microsoft.EntityFrameworkCore;
using Kanini.InvestmentSearchEngine.LoginWatchList.Mapper;
using System.Diagnostics.CodeAnalysis;

namespace Kanini.InvestmentSearchEngine.LoginWatchList
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region ConfigureServices
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("ReactCors", policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                });
            });

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AuthenticationAndWatchListContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("MyConn"));
            });

            builder.Services.AddScoped<IRepository<int, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int, WatchList>, WatchListRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWatchListService, WatchListService>();
            builder.Services.AddScoped<ITokenGenerate, TokenService>();
            builder.Services.AddScoped<IMapper, UserToUserDTO>();
            #endregion

            #region ConfigureAuthentication
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
            #endregion

            var app = builder.Build();

            #region ConfigureHttpRequestPipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("ReactCors");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
            #endregion
        }
    }
}
