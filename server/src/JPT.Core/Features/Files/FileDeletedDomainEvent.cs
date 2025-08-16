using JPT.Core.Common;

namespace JPT.Core.Features.Files;

/// <summary>
/// This event will find file by id to delete file
/// </summary>
public sealed record FileDeletedDomainEvent(Guid FileId) : IDomainEvent;
