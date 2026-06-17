using CatalogApi.Data;
using CatalogApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _db;
    public CategoryRepository(AppDbContext db) => _db = db;

    public Task<List<Category>> GetAllAsync() =>
        _db.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync();

    public Task<Category?> GetByIdAsync(int id) =>
        _db.Categories.FirstOrDefaultAsync(c => c.Id == id);

    public async Task AddAsync(Category category) =>
        await _db.Categories.AddAsync(category);

    public void Update(Category category) =>
        _db.Categories.Update(category);

    public void Remove(Category category) =>
        _db.Categories.Remove(category);

    public async Task<bool> SaveChangesAsync() =>
        await _db.SaveChangesAsync() > 0;
}