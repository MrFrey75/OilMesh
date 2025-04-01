using OilCore.Interfaces;
using Serilog;

namespace OilCore.Services;

public interface IOilLoggerService<T>
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex = null);
    void LogDebug(string message);
    void LogDebug(string message, object[] args);
    void LogCritical(string message, Exception? ex = null);
}

public class OilLoggerService<T> : IOilLoggerService<T>
{
    private readonly ILogger _logger;

    public OilLoggerService()
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.File(ConfigurationService.GetLogFile(), rollingInterval: RollingInterval.Day)
            .Enrich.WithProperty("SourceContext", typeof(T).FullName) // Associates logs with type T
            .CreateLogger();
    }

    public void LogInfo(string message) => 
        _logger.Information(message);

    public void LogWarning(string message) => 
        _logger.Warning(message);

    public void LogError(string message, Exception? ex = null) =>
        _logger.Error(ex, message);

    public void LogDebug(string message) => 
        _logger.Debug(message);
    
    public void LogDebug(string message, object[] args) => 
        _logger.Debug(message, args);

    public void LogCritical(string message, Exception? ex = null) =>
        _logger.Fatal(ex, message);
}