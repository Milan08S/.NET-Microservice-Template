using Microsoft.EntityFrameworkCore;
using PlantillaMicroservicio.Domain.Entities;
using PlantillaMicroservicio.Domain.Interfaces;
using PlantillaMicroservicio.Infrastructure.Data;

namespace PlantillaMicroservicio.Infrastructure.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entityToDelete = await _dbSet.FindAsync(id);
        if (entityToDelete != null)
        {
            _dbSet.Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }
    }
}