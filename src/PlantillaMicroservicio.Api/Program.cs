using Microsoft.EntityFrameworkCore;
using PlantillaMicroservicio.Application.Features.Products;
using PlantillaMicroservicio.Domain.Interfaces;
using PlantillaMicroservicio.Infrastructure.Data;
using PlantillaMicroservicio.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>
(options =>
    options.UseNpgsql(connectionString)
);

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "PlantillaMicroservicio API v1");
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "PlantillaMicroservicio API is running! Visit /swagger for documentation.");

app.Run();
