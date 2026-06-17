using CatalogApi.Models.Entities;

namespace CatalogApi.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
}