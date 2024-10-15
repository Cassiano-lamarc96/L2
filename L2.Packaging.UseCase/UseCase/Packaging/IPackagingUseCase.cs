namespace L2.Packaging.UseCase.UseCase.Packaging;

public interface IPackagingUseCase
{
    List<OrderPackagingResponse> Handle(List<OrderPackagingRequest> ordersRequest);
}
