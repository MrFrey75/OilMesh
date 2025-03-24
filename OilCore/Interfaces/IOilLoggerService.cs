namespace OilCore.Interfaces;

public interface IOilLoggerService
{
    void LogInfo(string message);
    void LogWarning(string message);
    void LogError(string message, Exception? ex = null);
    void LogDebug(string message);
    void LogCritical(string message, Exception? ex = null);
}