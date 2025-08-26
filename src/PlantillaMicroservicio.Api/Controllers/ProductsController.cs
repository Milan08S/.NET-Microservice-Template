using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantillaMicroservicio.Application.Features.Products;
using PlantillaMicroservicio.Application.Features.Products.DTOs;
using PlantillaMicroservicio.Domain.Entities;

namespace PlantillaMicroservicio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> PostProduct(CreateProductDto createProductDto)
    {
        var createdProductId = await _productService.AddProductAsync(createProductDto);
        return CreatedAtAction(nameof(GetProduct), new { id = createdProductId }, createProductDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.Id)
        {
            return BadRequest();
        }

        await _productService.UpdateProductAsync(product);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent(); 
    }
}
