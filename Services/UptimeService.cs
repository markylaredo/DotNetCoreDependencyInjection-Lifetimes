namespace DotNetCoreDependencyInjectionLifetimes.Services;

public class UptimeService
{
    private DateTime startTime;

    public UptimeService()
    {
        startTime = DateTime.UtcNow;
    }

    public TimeSpan GetUptime()
    {
        return DateTime.UtcNow - startTime;
    }
}