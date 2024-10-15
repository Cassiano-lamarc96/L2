using L2.Packaging.UseCase.UseCase.Packaging;
using Microsoft.Extensions.DependencyInjection;

namespace L2.Packaging.UseCase.Configuration;

public static class ConfigurationDI
{
    public static void AddUseCase(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPackagingUseCase, PackagingUseCase>();
    }
}
