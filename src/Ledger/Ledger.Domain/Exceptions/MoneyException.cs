using Shared.BuildingBlocks.Primitives;

namespace Ledger.Domain.Exceptions;

public sealed class MoneyException(string message) : DomainException(message);