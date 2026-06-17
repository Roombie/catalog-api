using CatalogApi.Models.Entities;

namespace CatalogApi.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task AddAsync(Category category);
    void Update(Category category);
    void Remove(Category category);
    Task<bool> SaveChangesAsync();
}