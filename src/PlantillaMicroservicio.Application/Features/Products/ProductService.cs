using AutoMapper;
using Microsoft.Extensions.Logging;
using PlantillaMicroservicio.Application.Features.Products.DTOs;
using PlantillaMicroservicio.Domain.Entities;
using PlantillaMicroservicio.Domain.Interfaces;

namespace PlantillaMicroservicio.Application.Features.Products;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;

    public ProductService(
        IProductRepository productRepository, 
        IMapper mapper,
        ILogger<ProductService> logger
    )
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        _logger.LogInformation("Searching product with ID {ProductId}", id);

        var productEntity = await _productRepository.GetByIdAsync(id);

        if (productEntity == null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found", id);
            return null;
        }

        return _mapper.Map<ProductDto>(productEntity);
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<int> AddProductAsync(CreateProductDto createProductDto)         
    {
        _logger.LogInformation("Adding new product with name {ProductName}", createProductDto.Name);

        if (createProductDto.Price < 0)
        {
            _logger.LogWarning("Attempt to add product with negative price: {Price}", createProductDto.Price);
            throw new ArgumentException("The price of the product cannot be negative.");
        }

        var productEntity = _mapper.Map<Product>(createProductDto);

        await _productRepository.AddAsync(productEntity);
        
        _logger.LogInformation("Product successfully added with ID {ProductId}", productEntity.Id);
        
        return productEntity.Id;
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