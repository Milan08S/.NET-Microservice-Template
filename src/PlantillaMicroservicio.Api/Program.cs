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

    builder.Host.UseSerilog(
        (context, services, configuration) => 
            configuration.ReadFrom.Configuration(context.Configuration));

    builder.Services.AddDbContext<AppDbContext>
        (options =>
            options.UseNpgsql(connectionString)
        );

    builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString);

    // Dependency Injection
    builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductService, ProductService>();



    builder.Services.AddControllers();

    // Validators
    builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();

    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

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

