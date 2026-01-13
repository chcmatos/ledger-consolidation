using Consolidation.Application.DependencyInjection;
using Consolidation.Infrastructure.DependencyInjection;
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

app.ApplyDatabaseMigration();

app.Run();
