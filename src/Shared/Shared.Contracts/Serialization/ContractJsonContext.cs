using System.Text.Json.Serialization;

namespace Shared.Contracts.Serialization;

/// <summary>
/// Source-generated JSON metadata for contracts to reduce allocations and improve performance.
/// </summary>
[JsonSerializable(typeof(Shared.Contracts.Events.TransactionPostedV1))]
public partial class ContractJsonContext : JsonSerializerContext;
