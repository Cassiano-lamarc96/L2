namespace L2.Packaging.Domain.Models;

public class Box
{
    public Box(string name)
    {
        Name = name;
    }

    public Box(string name, List<Product> products)
    {
        Name = name;
        Products = products;
    }

    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
