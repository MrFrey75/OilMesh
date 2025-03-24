using System.Text.Json;
using OilCore.Interfaces;
using OilCore.Models;

namespace OilCore.Services;

public class AppConfigService : IAppConfigService
{
    private readonly IOilLoggerService _logger;
    private readonly string _configFilePath;
    private readonly Lock _lock = new();
    private AppConfig? _appConfig;
    
    public event Action<AppConfig>? OnConfigUpdated;

    public AppConfigService(IOilLoggerService logger, string? configFilePath = null)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _configFilePath = configFilePath ?? ConfigurationService.GetConfigurationFile();
        _logger.LogInfo($"AppConfigService initialized with config path: {_configFilePath}");
    }

    public AppConfig GetConfig()
    {
        EnsureConfigLoaded();
        lock (_lock)
        {
            return _appConfig!;
        }
    }

    public void SetConfig(AppConfig newConfig)
    {
        if (newConfig == null) throw new ArgumentNullException(nameof(newConfig));

        lock (_lock)
        {
            _logger.LogInfo("Updating AppConfig");
            _appConfig = newConfig;
            WriteConfig();
            OnConfigUpdated?.Invoke(_appConfig);
        }
    }

    public void SetAppName(string appName)
    {
        if (string.IsNullOrWhiteSpace(appName)) throw new ArgumentException("App name cannot be null or whitespace");

        EnsureConfigLoaded();

        lock (_lock)
        {
            _logger.LogInfo($"Setting AppName to: {appName}");
            _appConfig!.AppName = appName;
            WriteConfig();
            OnConfigUpdated?.Invoke(_appConfig);
        }
    }

    private void EnsureConfigLoaded()
    {
        if (_appConfig != null) return;

        lock (_lock)
        {
            if (_appConfig != null) return;

            _logger.LogInfo("Attempting to load AppConfig from disk");

            try
            {
                if (!File.Exists(_configFilePath))
                {
                    _logger.LogWarning("Config file not found. Creating default AppConfig.");
                    _appConfig = new AppConfig("DefaultApp");
                    WriteConfig();
                    return;
                }

                var json = File.ReadAllText(_configFilePath);
                _appConfig = JsonSerializer.Deserialize<AppConfig>(json);

                if (_appConfig == null)
                {
                    throw new InvalidOperationException("Failed to deserialize AppConfig");
                }

                _logger.LogInfo("AppConfig successfully loaded.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to load AppConfig. Creating fallback config.", ex);
                _appConfig = new AppConfig("FallbackApp");
                WriteConfig();
            }
        }
    }

    private void WriteConfig()
    {
        try
        {
            var json = JsonSerializer.Serialize(_appConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configFilePath, json);
            _logger.LogInfo("AppConfig successfully written to disk.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to write AppConfig to disk", ex);
            throw;
        }
    }
}
