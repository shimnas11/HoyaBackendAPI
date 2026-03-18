using System.Net;
using System.Text.Json.Serialization;
using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Application.Mappings;
using Hoya.Inventory.Domain.Configurations;
using Hoya.Inventory.Domain.Interfaces;
using Hoya.Inventory.Infrastructure.Mongo;
using Hoya.Inventory.Infrastructure.Repository;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("hoyaApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Replace with your Angular app's exact URL and port
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});


builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddMapster();

var config = TypeAdapterConfig.GlobalSettings;
config.Scan(typeof(ProductMappingConfig).Assembly);

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

//builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
// Configure the HTTP request pipeline.
app.UseCors("hoyaApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
