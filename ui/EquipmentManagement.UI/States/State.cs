namespace EquipmentManagement.UI.States;

public abstract class State<T> : IEquatable<State<T>>, IEquatable<string>
    where T : State<T>

{
    public string Name { get; set; }

    protected State(string name)
    {
        Name = name;
    }

    public bool Equals(State<T>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        if(obj is not State<T> other)
            return false;
        return GetType().Name == other.Name
           && Name == other.Name;
    }

    public bool Equals(string? other)
    {
        return Name == other;
    }

    public override int GetHashCode() 
        => Name.GetHashCode();
    
}


