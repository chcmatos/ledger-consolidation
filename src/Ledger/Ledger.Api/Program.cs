using Ledger.Infrastructure.DependencyInjection;
using Ledger.Application.DependencyInjection;
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

app.ApplyDatabaseMigration();

app.Run();
