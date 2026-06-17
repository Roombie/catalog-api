namespace CatalogApi.Models.Dtos;

public record CategoryDto(int Id, string Name, DateTime CreatedAt);
public record CategoryCreateDto(string Name);