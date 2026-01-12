using Consolidation.Application.DependencyInjection;
using Consolidation.Infrastructure.DependencyInjection;
using Consolidation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.BuildingBlocks.Resilience;
using Shared.Auth.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices(
    title: "Consolidation API",
    description: "API para gerenciamento de consolidação de lançamentos financeiros"
);

builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddConsolidationInfrastructure(builder.Configuration);
builder.Services.AddConsolidationUseCases();

builder.Services.AddControllers();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

Retry.With(
    action: () =>
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ConsolidationDbContext>();
        db.Database.Migrate();
    },
    maxAttempts: 15,
    delay: TimeSpan.FromSeconds(2),
    onRetry: (attempt, ex) =>
    {
        app.Logger.LogWarning(ex, "Consolidation DB not ready. Retry {Attempt}/15.", attempt);
    });

app.Run();
