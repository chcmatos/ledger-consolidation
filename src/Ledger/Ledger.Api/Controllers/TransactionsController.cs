using Ledger.Application.UseCases;
using Ledger.Application.UseCases.Commands;
using Ledger.Application.UseCases.Queries;
using Ledger.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Ledger.Application.UseCases.Results;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Ledger.Api.Controllers;

[Authorize]
[ApiController]
[Route("transactions")]
public sealed class TransactionsController(
    ICreateTransactionUseCase createTransactionUseCase,
    IFindTransactionUseCase findTransactionUseCase,
    IListTransactionUseCase listTransactionUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateTransactionResponse>> Create([FromBody] CreateTransactionRequest req, CancellationToken ct)
    {
        var cmd = new CreateTransactionCommand(req.BusinessDate, req.Amount, req.Type, req.Description);
        var result = await createTransactionUseCase.HandleAsync(cmd, ct);
        var uri = Url.Action(nameof(GetTransactionById), new { result.TransactionId });
        return Created(uri, new CreateTransactionResponse(result.TransactionId));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransactionResult>> GetTransactionById([FromRoute] Guid id, CancellationToken ct)
    {
        var result = await findTransactionUseCase.HandleAsync(new FindTransactionQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransactionResult>>> List([FromQuery] DateOnly date, CancellationToken ct)
    {
        var items = await listTransactionUseCase.HandleAsync(new ListTransactionQuery(date), ct);
        return Ok(items);
    }

    [HttpGet("paginate")]
    public async Task<ActionResult<IEnumerable<TransactionResult>>> List(
        [Required] [FromQuery] DateOnly date, 
        [Required] [FromQuery] int pageNumber, 
        [Required] [FromQuery] int pageSize, CancellationToken ct)
    {
        var items = await listTransactionUseCase.HandleAsync(new ListTransactionQuery(date, pageNumber, pageSize), ct);
        return Ok(items);
    }

}
