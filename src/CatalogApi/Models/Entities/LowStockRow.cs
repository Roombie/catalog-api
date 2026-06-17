namespace CatalogApi.Models.Entities;

public class LowStockRow
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public int Stock { get; set; }
    public decimal Price { get; set; }
}