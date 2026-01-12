using Ledger.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace Ledger.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLedgerUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        services.AddScoped<IFindTransactionUseCase, FindTransactionUseCase>();
        services.AddScoped<IListTransactionUseCase, ListTransactionUseCase>();
        return services;
    }
}