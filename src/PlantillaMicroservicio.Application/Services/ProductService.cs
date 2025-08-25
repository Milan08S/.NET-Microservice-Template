using PlantillaMicroservicio.Application.Interfaces;
using PlantillaMicroservicio.Domain.Interfaces;
using PlantillaMicroservicio.Domain.Entities;

namespace PlantillaMicroservicio.Application.Services;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task AddProductAsync(Product product)
    {
        if (product.Price < 0)
        {
            throw new ArgumentException("El precio del producto no puede ser negativo.");
        }

        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}