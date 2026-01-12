namespace Consolidation.Application.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;
using Consolidation.Application.UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsolidationUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetDailyBalanceUseCase, GetDailyBalanceUseCase>();
        return services;
    }
}