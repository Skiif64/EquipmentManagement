using EquipmentManagement.UI.Abstractions;
using EquipmentManagement.UI.Authentification;

namespace EquipmentManagement.UI.States;

public abstract class TokenStorageState : State
{
    public abstract Func<IServiceProvider, ITokenStorage> Factory { get; }
    protected TokenStorageState(string name) : base(name)
    {
    }

    public static TokenStorageState Session => new SessionTokenState();
    public static TokenStorageState Local => new LocalTokenState();
    private class SessionTokenState : TokenStorageState
    {
        public override Func<IServiceProvider, ITokenStorage> Factory { get; } = sp
            => sp.GetRequiredService<SessionTokenStorage>();
        public SessionTokenState() : base("Session")
        {
        }

        
    }

    private class LocalTokenState : TokenStorageState
    {
        public override Func<IServiceProvider, ITokenStorage> Factory { get; } = sp
            => sp.GetRequiredService<LocalTokenStorage>();
        public LocalTokenState() : base("Local")
        {
        }


    }

}
