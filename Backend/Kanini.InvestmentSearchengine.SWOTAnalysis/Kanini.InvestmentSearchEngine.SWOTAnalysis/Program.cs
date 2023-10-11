using Kanini.InvestmentSearchEngine.SWOTAnalysis.Context;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Interfaces;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Mapper;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Models;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Repositories;
using Kanini.InvestmentSearchEngine.SWOTAnalysis.Services;
using Microsoft.EntityFrameworkCore;


namespace Kanini.InvestmentSearchEngine.SWOTAnalysis
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
            #region SQL Connection
            builder.Services.AddDbContext<SWOTContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));
            #endregion
            #region Services
            builder.Services.AddScoped<IRepository<int, SWOT>, SWOTRepository>();
            builder.Services.AddScoped<ISWOTService, SWOTService>();
           builder.Services.AddScoped<IMapper, SwotTOSwotDto>();
            #endregion
           
            #region Cors
            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("CORS", options =>
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
            app.UseCors("CORS");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}