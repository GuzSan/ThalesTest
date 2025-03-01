namespace ProductosMVC.Models;

public class Product
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public Category? Category { get; set; }
    public List<string>? Images { get; set; }
    public DateTime CreationAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal Tax { get; set; }
    public decimal PriceWithTaxes { get; set; }
}

public class Category {
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Image { get; set; }
    public DateTime CreationAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}