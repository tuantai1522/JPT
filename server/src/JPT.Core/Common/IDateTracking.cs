namespace JPT.Core.Common;

public interface IDateTracking
{
    public long CreatedAt { get; }
    
    public long? UpdatedAt { get; }
}