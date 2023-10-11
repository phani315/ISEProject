using Kanini.InvestmentSearchEngine.CompanyDetails.Interfaces;
using Kanini.InvestmentSearchEngine.CompanyDetails.Models;
using Kanini.InvestmentSearchEngine.CompanyDetails.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Kanini.InvestmentSearchEngine.CompanyDetails.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>
               (options => options.UseSqlServer(builder.Configuration.GetConnectionString("myConn")));
builder.Services.AddCors(opts =>                    
{
    opts.AddPolicy("CORS", options =>
    {
        options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

builder.Services.AddScoped<IRepo<int, CompanyDetail>, CompanyDetailsRepository>();
builder.Services.AddScoped<ICompanyDetailsServices, CompanyDetailsServices>();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

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
