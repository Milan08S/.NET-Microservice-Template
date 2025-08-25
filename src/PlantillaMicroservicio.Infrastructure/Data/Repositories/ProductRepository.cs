using Microsoft.EntityFrameworkCore;
using PlantillaMicroservicio.Domain.Interfaces;
using PlantillaMicroservicio.Domain.Entities;

namespace PlantillaMicroservicio.Infrastructure.Data.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

}