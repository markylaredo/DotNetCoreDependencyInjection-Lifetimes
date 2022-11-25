namespace DotNetCoreDependencyInjectionLifetimes.Interfaces;

public sealed class Session : ISingletonService,
    ITransientService,
    IScopedService
{
    Guid _guid;

    public Session()
    {
        _guid = Guid.NewGuid();
    }

    public Guid GetSessionId()
    {
        return _guid;
    }
}

public sealed class SessionService 
{
    public SessionService(ITransientService transientSession, 
        IScopedService scopedSession,
        ISingletonService singletonSession)
    {
        TransientSession = transientSession;
        ScopedSession = scopedSession;
        SingletonSession = singletonSession;
    }

    public ITransientService TransientSession { get; }
    public IScopedService ScopedSession { get; }
    public ISingletonService SingletonSession { get; }

 
}