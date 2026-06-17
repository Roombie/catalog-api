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
        return items.Select(c => new CategoryDto(c.Id, c.Name, c.CreatedAt)).ToList();
    }
}