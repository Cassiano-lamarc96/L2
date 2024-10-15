using L2.Packaging.Domain.Entity;
using L2.Packaging.Domain.Models;

namespace L2.Packaging.UseCase.UseCase.Packaging;

public class PackagingUseCase : IPackagingUseCase
{
    public List<OrderPackagingResponse> Handle(List<OrderPackagingRequest> ordersRequest)
    {
        // Tamanho de caixas existentes na base de dados
        List<AllowedBox> allowedBoxes = new List<AllowedBox>()
        {
            new AllowedBox("Caixa 1", 30M, 40M, 80M),
            new AllowedBox("Caixa 2", 80M, 50M, 40M),
            new AllowedBox("Caixa 3", 50M, 60M, 80M),
        };

        List<OrderPackagingResponse> orderPackagingResponses = new();
        foreach (OrderPackagingRequest order in ordersRequest)
        {
            List<Product> productsOusideBox = new();
            foreach (var product in order.Products)
            {
                var boxesWithValidVolume = allowedBoxes.Where(x => x.GetVolume() <= product.GetVolume()).ToList();
                if (boxesWithValidVolume == null || boxesWithValidVolume.Count == 0)
                {
                    productsOusideBox.Add(new Product(product.name));
                    continue;
                }

                var boxesThatFitsInTheBox = boxesWithValidVolume.Where(x => x.FitsInTheBox(product.height, product.width, product.length));
                if (boxesThatFitsInTheBox == null || boxesWithValidVolume.Count == 0)
                {
                    productsOusideBox.Add(new Product(product.name));
                    continue;
                }

                var choosenBoxes = boxesThatFitsInTheBox.OrderBy(x => x.GetNewVolume(product.height, product.width, product.length)).FirstOrDefault();
                
                orderPackagingResponses.Add(
                    new OrderPackagingResponse()
                    {
                        Boxes = new List<Box>()
                        {
                            new Box(choosenBoxes?.Name!)
                            {
                                Products = new List<Product>
                                {
                                    new Product(product.name, product.height, product.width, product.length)
                                }
                            }
                        },
                        ProductsOutsideBox = productsOusideBox
                    });
            }
        }

        return orderPackagingResponses;
    }
}
