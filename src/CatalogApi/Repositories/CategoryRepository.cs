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
}