using System.Runtime.InteropServices;
using OilCore.Utilities;

namespace OilCore.Services;

public static class ConfigurationService
{
    private static bool _isInitialized;
    private static string? AppName { get; set; }
    private static string? BasePath { get; set; }
    private static string? TempPath { get; set; }
    private static string? UploadPath { get; set; }
    private static string? AssetsPath { get; set; }

    // Logs
    private static string? LogPath { get; set; }
    private static string? LogFile { get; set; }

    // Database
    private static string? DatabasePath { get; set; }
    private static string? DatabaseFile { get; set; }

    // Configuration
    private static string? ConfigurationPath { get; set; }
    private static string? ConfigurationFile { get; set; }

    public static void Reset()
    {
        _isInitialized = false;
        FileDirectoryHelper.DeleteDirectory(GetBasePath());
    }

    private static void EnsureInitialized()
    {
        if (!_isInitialized)
            throw new Exception("Configuration is not initialized.");
    }

    private static string EnsurePath(string? path, string errorMessage)
    {
        EnsureInitialized();
        return path ?? throw new Exception(errorMessage);
    }

    private static string GetBasePath() => EnsurePath(BasePath, "Base Path is not set.");
    public static string GetAssetsPath() => EnsurePath(AssetsPath, "Assets Path is not set.");
    public static string GetUploadPath() => EnsurePath(UploadPath, "Upload Path is not set.");
    public static string GetTempPath() => EnsurePath(TempPath, "Temp Path is not set.");
    public static string GetAppName() => EnsurePath(AppName, "App Name is not set.");
    public static string GetLogPath() => EnsurePath(LogPath, "Log Path is not set.");
    public static string GetLogFile() => EnsurePath(LogFile, "Log File is not set.");
    public static string GetDatabasePath() => EnsurePath(DatabasePath, "Database Path is not set.");
    public static string GetDatabaseFile() => EnsurePath(DatabaseFile, "Database File is not set.");
    public static string GetConfigurationPath() => EnsurePath(ConfigurationPath, "Configuration Path is not set.");
    public static string GetConfigurationFile() => EnsurePath(ConfigurationFile, "Configuration File is not set.");

    public static string GetConnectionString() =>
        $"Data Source={EnsurePath(DatabaseFile, "Database Connection is not set.")}";

    public static void Initialize(string appName)
    {
        if (string.IsNullOrWhiteSpace(appName))
            throw new ArgumentNullException(nameof(appName));

        try
        {
            AppName = appName;
            Console.WriteLine($"{AppName} Configuration Service Initializing...");

            BasePath = DetermineBasePath();
            LogPath = Path.Combine(BasePath, "Logs");
            LogFile = Path.Combine(LogPath, "events.log");
            DatabasePath = Path.Combine(BasePath, "Data");
            DatabaseFile = Path.Combine(DatabasePath, $"{AppName}Db.sqlite");
            TempPath = Path.Combine(BasePath, "Temp");
            UploadPath = Path.Combine(BasePath, "Uploads");
            AssetsPath = Path.Combine(BasePath, "Assets");
            ConfigurationPath = Path.Combine(BasePath, "Config");
            ConfigurationFile = Path.Combine(ConfigurationPath, $"{AppName}.json");

            CreateRequiredDirectories();
            _isInitialized = true;

            Console.WriteLine($"{AppName} Configuration Service Initialized.");
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to initialize configuration service.", ex);
        }
    }

    private static void CreateRequiredDirectories()
    {
        FileDirectoryHelper.CreateDirectory(BasePath!);
        FileDirectoryHelper.CreateDirectory(DatabasePath!);
        FileDirectoryHelper.CreateDirectory(LogPath!);
        FileDirectoryHelper.CreateFile(LogFile!);
        FileDirectoryHelper.CreateDirectory(TempPath!);
        FileDirectoryHelper.CreateDirectory(UploadPath!);
        FileDirectoryHelper.CreateDirectory(AssetsPath!);
        FileDirectoryHelper.CreateDirectory(ConfigurationPath!);
        FileDirectoryHelper.CreateFile(ConfigurationFile!, "{}");
    }

    private static string DetermineBasePath()
    {
        try
        {
            string basePath = Environment.OSVersion.Platform switch
            {
                PlatformID.Win32NT => Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                    CoreConstants.CoreName, $"{CoreConstants.CoreName}.{AppName}"),

                PlatformID.Unix when RuntimeInformation.IsOSPlatform(OSPlatform.OSX) => Path.Combine(
                    "/usr/local/var", CoreConstants.CoreName, $"{CoreConstants.CoreName}.{AppName}"),

                PlatformID.Unix => Path.Combine(
                    "/var/lib", CoreConstants.CoreName, $"{CoreConstants.CoreName}.{AppName}"),

                _ => throw new PlatformNotSupportedException("The current operating system is not supported.")
            };

            FileDirectoryHelper.CreateDirectory(basePath);
            return basePath;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while determining the base path.", ex);
        }
    }
}
