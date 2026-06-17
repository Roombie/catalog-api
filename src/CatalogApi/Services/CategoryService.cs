using CatalogApi.Models.Dtos;
using CatalogApi.Models.Entities;
using CatalogApi.Repositories;

namespace CatalogApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;
    public CategoryService(ICategoryRepository repo) => _repo = repo;

    public async Task<List<CategoryDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(ToDto).ToList();
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        return category is null ? null : ToDto(category);
    }

    public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = new Category { Name = dto.Name.Trim() };
        await _repo.AddAsync(category);
        await _repo.SaveChangesAsync();
        return ToDto(category);
    }

    public async Task<bool> UpdateAsync(int id, CategoryCreateDto dto)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category is null) return false;

        category.Name = dto.Name.Trim();
        _repo.Update(category);
        return await _repo.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category is null) return false;

        _repo.Remove(category);
        return await _repo.SaveChangesAsync();
    }

    private static CategoryDto ToDto(Category c) => new(c.Id, c.Name, c.CreatedAt);
}