namespace EquipmentManagement.Domain.Models.Base;

public abstract class BaseModel : IEquatable<BaseModel>
{
    public Guid Id { get; }

    public BaseModel()
    {
        Id = Guid.NewGuid();
    }

    public BaseModel(Guid id)
    {
        Id = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not BaseModel other)
            return false;
        return Id == other.Id;
    }

    public bool Equals(BaseModel? other)
    {
        if (other is null)
            return false;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
