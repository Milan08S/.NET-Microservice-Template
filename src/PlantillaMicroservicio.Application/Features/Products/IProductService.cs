using PlantillaMicroservicio.Domain.Entities;
using PlantillaMicroservicio.Application.Features.Products.DTOs;

namespace PlantillaMicroservicio.Application.Features.Products;
public interface IProductService
{
    Task<ProductDto?> GetProductByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
}