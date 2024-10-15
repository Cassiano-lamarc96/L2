using L2.Packaging.Domain.Entity;
using L2.Packaging.Domain.Models;

namespace L2.Packaging.UseCase.UseCase.Packaging;

public class PackagingUseCase : IPackagingUseCase
{
    // Caixas existentes na base de dados
    private readonly List<AllowedBox> _dbBoxes = new List<AllowedBox>()
        {
            new AllowedBox("Caixa 1", 30M, 40M, 80M),
            new AllowedBox("Caixa 2", 80M, 50M, 40M),
            new AllowedBox("Caixa 3", 50M, 60M, 80M),
        };
    private List<AllowedBox>? SameBox;
    private string? ChoosenBoxName { get; set; }

    public List<OrderPackagingResponse> Handle(List<OrderPackagingRequest> ordersRequest)
    {
        List<OrderPackagingResponse> orderPackagingResponses = new();
        foreach (OrderPackagingRequest order in ordersRequest)
        {
            OrderPackagingResponse orderPackagingResponse = new();
            List<Product> productsOusideBox = new();
            OrderPackagingResponse response = new();
            List<Product> validProducts = new();

            foreach (var product in order.products)
            {
                if (SameBox != null)
                {
                    var sameBoxWithValidVolume = SameBox.Where(x => x.GetVolume() >= product.GetVolume()).ToList();
                    if (sameBoxWithValidVolume != null && sameBoxWithValidVolume.Count > 0)
                    {
                        var sameBoxThatFitsInTheBox = sameBoxWithValidVolume.Where(x => x.FitsInTheBox(product.height, product.width, product.length)).ToList();
                        if (sameBoxThatFitsInTheBox != null && sameBoxThatFitsInTheBox.Count > 0)
                        {
                            var choosenSameBox = sameBoxThatFitsInTheBox.OrderBy(x => x.GetNewVolume(product.height, product.width, product.length)).FirstOrDefault();
                            SameBox = choosenSameBox.GetNewVolume(product.height, product.width, product.length) > 0 && order.products.LastOrDefault() != product ?
                                choosenSameBox.GetRemainingSpaces(product.height, product.width, product.length) : null!;

                            validProducts.Add(new Product(product.name, product.height, product.width, product.length));
                            if (SameBox == null)
                                response.Boxes.Add(new Box(ChoosenBoxName, validProducts));

                            continue;
                        }
                    }
                }

                var boxesWithValidVolume = _dbBoxes.Where(x => x.GetVolume() >= product.GetVolume()).ToList();
                if (boxesWithValidVolume == null || boxesWithValidVolume.Count == 0)
                {
                    productsOusideBox.Add(new Product(product.name, product.height, product.width, product.length));
                    continue;
                }

                var boxesThatFitsInTheBox = boxesWithValidVolume.Where(x => x.FitsInTheBox(product.height, product.width, product.length)).ToList();
                if (boxesThatFitsInTheBox == null || boxesThatFitsInTheBox.Count == 0)
                {
                    productsOusideBox.Add(new Product(product.name, product.height, product.width, product.length));
                    continue;
                }

                var choosenBox = boxesThatFitsInTheBox.OrderBy(x => x.GetNewVolume(product.height, product.width, product.length)).FirstOrDefault();

                SameBox = choosenBox.GetNewVolume(product.height, product.width, product.length) > 0 && order.products.LastOrDefault() != product ?
                    choosenBox.GetRemainingSpaces(product.height, product.width, product.length) : null;
                ChoosenBoxName = choosenBox.Name;

                validProducts.Add(new Product(product.name, product.height, product.width, product.length));
                if (SameBox == null)
                    response.Boxes.Add(new Box(ChoosenBoxName, validProducts));
            }

            response.OrderId = order.orderId;
            response.ProductsOutsideBox = productsOusideBox;

            orderPackagingResponses.Add(response);
        }

        return orderPackagingResponses;
    }
}
