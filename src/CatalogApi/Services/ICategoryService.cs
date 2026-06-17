using CatalogApi.Models.Dtos;

namespace CatalogApi.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
}