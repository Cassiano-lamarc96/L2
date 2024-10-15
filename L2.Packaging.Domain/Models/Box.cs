namespace L2.Packaging.Domain.Models;

public class Box
{
    public Box(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
