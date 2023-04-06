using System.Collections.Concurrent;

namespace EquipmentManagement.UI.States;

public class StateStorage
{
    private readonly ConcurrentDictionary<Type, State> _states = new();
    public void SetState<T>(Type type, T state) where T : State
    {       
        _states.AddOrUpdate(type, state, (key, oldValue) => oldValue = state);
    }

    public T GetState<T>() where T : State
    {
        var stateType = typeof(T);
        if (!_states.TryGetValue(stateType, out var value))
            throw new ArgumentException($"State with type {stateType.Name} not containing.");

        return (T)value;
    }
}
