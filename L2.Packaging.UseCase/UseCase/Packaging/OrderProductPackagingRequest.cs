namespace L2.Packaging.UseCase.UseCase.Packaging;


public record OrderProductPackagingRequest(string name, decimal height, decimal width, decimal length)
{
    public decimal GetVolume()
    {
        return height * width * length;
    }
}
