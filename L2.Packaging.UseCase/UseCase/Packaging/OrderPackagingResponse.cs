using L2.Packaging.Domain.Models;

namespace L2.Packaging.UseCase.UseCase.Packaging;

public class OrderPackagingResponse
{
    public OrderPackagingResponse()
    {
        Boxes = new List<Box>();    
    }

    public int OrderId { get; set; }
    public List<Box> Boxes { get; set; }
    public List<Product> ProductsOutsideBox { get; set; }
}
