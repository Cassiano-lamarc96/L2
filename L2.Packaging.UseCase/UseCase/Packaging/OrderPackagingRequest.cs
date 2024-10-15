namespace L2.Packaging.UseCase.UseCase.Packaging;

public record OrderPackagingRequest(List<OrderProductPackagingRequest> products, int orderId);
