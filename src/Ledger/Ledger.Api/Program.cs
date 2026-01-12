using Ledger.Infrastructure.DependencyInjection;
using Ledger.Infrastructure.Persistence;
using Ledger.Application.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Shared.BuildingBlocks.Resilience;
using Shared.Auth.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices(
    title: "Ledger API",
    description: "API para gerenciamento de lançamentos financeiros"
);

builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddLedgerInfrastructure(builder.Configuration);
builder.Services.AddLedgerUseCases();

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
        var db = scope.ServiceProvider.GetRequiredService<LedgerDbContext>();
        db.Database.Migrate();
    },
    maxAttempts: 15,
    delay: TimeSpan.FromSeconds(2),
    onRetry: (attempt, ex) =>
    {
        app.Logger.LogWarning(ex, "Ledger DB not ready. Retry {Attempt}/15.", attempt);
    });

app.Run();
