using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PlantillaMicroservicio.Api.Middleware;
using PlantillaMicroservicio.Application.Features.Products;
using PlantillaMicroservicio.Application.Features.Products.Validators;
using PlantillaMicroservicio.Application.Mappings;
using PlantillaMicroservicio.Domain.Interfaces;
using PlantillaMicroservicio.Infrastructure.Data;
using PlantillaMicroservicio.Infrastructure.Data.Repositories;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    builder.Services.AddDbContext<AppDbContext>
    (options =>
        options.UseNpgsql(connectionString)
    );

    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();

    builder.Services.AddHealthChecks()
        .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"));

    builder.Services.AddControllers();

    builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddOpenApi();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlerMiddleware>();

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

    app.MapHealthChecks("/health");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}

