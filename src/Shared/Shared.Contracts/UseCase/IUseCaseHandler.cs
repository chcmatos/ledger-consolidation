namespace Shared.Contracts.UseCase;

/// <summary>
/// Abstraction for handling use cases.
/// </summary>
/// <typeparam name="TRequest">Type of the use case request.</typeparam>
/// <typeparam name="TResponse">Type of the use case response.</typeparam>
public interface IUseCaseHandler<TRequest, TResponse>
{
    /// <summary>
    /// Handles the use case asynchronously.
    /// </summary>
    /// <param name="request">The use case request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The use case response.</returns>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}