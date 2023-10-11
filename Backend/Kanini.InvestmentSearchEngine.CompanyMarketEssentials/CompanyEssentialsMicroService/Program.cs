using KaniniInvestmentSearchEngineCompanyMarketEssentials.Context;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Interfaces;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Models;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Repositary;
using KaniniInvestmentSearchEngineCompanyMarketEssentials.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace KaniniInvestmentSearchEngineCompanyMarketEssentials
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region Cors
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("CORS", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            #endregion

            #region SQL Connection
            builder.Services.AddDbContext<CompanyEssentialsContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Conn");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    options.UseSqlServer(connectionString);
                }
                else
                {
                    throw new Exception();
                }
            });
            #endregion

            #region Services
            builder.Services.AddScoped<IRepository<int, CompanyEssentials>, CompanyEssentialsRepository>();
            builder.Services.AddScoped<ICompanyEssentialsServices<int,CompanyEssentials>,CompanyEssentialsServices>();
            #endregion

            #region SeriLog
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
            builder.Host.UseSerilog();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            } 
            app.UseCors("CORS");
            app.UseSerilogRequestLogging();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
} 
