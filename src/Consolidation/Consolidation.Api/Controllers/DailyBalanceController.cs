using Consolidation.Application.UseCases;
using Consolidation.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Consolidation.Api.Controllers;

[Authorize]
[ApiController]
[Route("daily-balance")]
public sealed class DailyBalanceController(IGetDailyBalanceUseCase usecase) : ControllerBase
{
    /// <summary>
    /// Gets the consolidated daily balance for a given business date.
    /// </summary>
    /// <param name="date">The business date for which to retrieve the balance.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The consolidated daily balance.</returns>
    [HttpGet]
    public async Task<ActionResult<DailyBalanceResponse>> Get([FromQuery] DateOnly date, CancellationToken ct)
    {
        var result = await usecase.HandleAsync(new GetDailyBalanceQuery(date), ct);
        return result is null ? NotFound() : Ok(new DailyBalanceResponse(result.BusinessDate, result.CreditTotal, result.DebitTotal, result.Amount));
    }
}
