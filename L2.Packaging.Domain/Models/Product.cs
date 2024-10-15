namespace L2.Packaging.Domain.Models;

public class Product
{
    public Product(string name)
    {
        Name = name;
    }

    public Product(string name, decimal height, decimal width, decimal length)
    {
        Name = name;
        Height = height;
        Width = width;
        Length = length;
    }

    public string Name { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; }
}
