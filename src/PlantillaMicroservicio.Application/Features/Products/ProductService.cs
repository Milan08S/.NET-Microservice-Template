using PlantillaMicroservicio.Domain.Interfaces;
using PlantillaMicroservicio.Domain.Entities;
using PlantillaMicroservicio.Application.Features.Products;
using PlantillaMicroservicio.Application.Features.Products.DTOs;

namespace PlantillaMicroservicio.Application.Features.Products;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var productEntity = await _productRepository.GetByIdAsync(id);

        if (productEntity == null)
        {
            return null;
        }

        var productDto = new ProductDto
        {
            Id = productEntity.Id,
            Name = productEntity.Name,
            Price = productEntity.Price
        };

        return productDto;
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