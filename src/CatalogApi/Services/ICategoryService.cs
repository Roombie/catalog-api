using CatalogApi.Models.Dtos;

namespace CatalogApi.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(int id);
    Task<CategoryDto> CreateAsync(CategoryCreateDto dto);
    Task<bool> UpdateAsync(int id, CategoryCreateDto dto);
    Task<bool> DeleteAsync(int id);
}