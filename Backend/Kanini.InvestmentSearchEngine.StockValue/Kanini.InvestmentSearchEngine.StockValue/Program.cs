using Kanini.InvestmentSearchEngine.StockValue.Interfaces;
using Kanini.InvestmentSearchEngine.StockValue.Models.Context;
using Kanini.InvestmentSearchEngine.StockValue.Models;
using Kanini.InvestmentSearchEngine.StockValue.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Kanini.InvestmentSearchEngine.StockValue.Services;
using System.Diagnostics.CodeAnalysis;
using Kanini.InvestmentSearchEngine.StockValue.Utilities.Mapper;

public partial class Program{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        #region Controllers and Swagger Injection
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                         {
                         {
                            new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                            new string[] {}
                         }
            });
        });
        #endregion

        #region Context injection
        builder.Services.AddDbContext<StockPriceContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
        #endregion

        #region Repo and Service Injection
        builder.Services.AddScoped<IRepository<int, StockPrice>, StockPriceRepository>();
        builder.Services.AddScoped<IRepository<int, StockTransaction>, StockTransactionRepository>();
        builder.Services.AddScoped<IStockPriceService, StockPriceService>();
        builder.Services.AddScoped<IMapperService, MapperService>();
        #endregion

        #region AuthenticationScheme
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

        #region HTTP and CORS Injection
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("ReactCORS", options =>
            {
                options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });
        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("ReactCORS");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}

[ExcludeFromCodeCoverage]
public partial class Program{ }

