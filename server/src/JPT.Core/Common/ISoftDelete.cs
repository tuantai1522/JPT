namespace JPT.Core.Common;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    
    public long? DeletedAt { get; set; }

    /// <summary>
    /// To reverse previous operation.
    /// </summary>
    public void Undo()
    {
        IsDeleted = !IsDeleted;
        DeletedAt = IsDeleted ? null : DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }
}