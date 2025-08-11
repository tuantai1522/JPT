namespace JPT.Core.Common;

public interface ISoftDelete
{
    public bool IsDeleted { get; }

    public long? DeletedAt { get; }

    public void Delete();
}