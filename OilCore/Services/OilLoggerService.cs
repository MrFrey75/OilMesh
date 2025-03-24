using OilCore.Interfaces;
using Serilog;

namespace OilCore.Services;

public class OilLoggerService : IOilLoggerService
{
    private readonly ILogger _logger;

    public OilLoggerService(string appName)
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.Debug()
            .WriteTo.File(ConfigurationService.GetLogFile(), rollingInterval: RollingInterval.Day)
            .Enrich.WithProperty("Application", appName)
            .CreateLogger();
    }

    public void LogInfo(string message) => _logger.Information(message);

    public void LogWarning(string message) => _logger.Warning(message);

    public void LogError(string message, Exception? ex = null) =>
        _logger.Error(ex, message);

    public void LogDebug(string message) => _logger.Debug(message);

    public void LogCritical(string message, Exception? ex = null) =>
        _logger.Fatal(ex, message);

    
}