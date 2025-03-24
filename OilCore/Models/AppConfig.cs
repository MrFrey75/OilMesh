namespace OilCore.Models;

public class AppConfig
{
    
    public string AppName { get; set; }
    public string AppVersion { get; set; }
    public string AppDescription { get; set; }

    public AppConfig(string appName)
    {
        AppName = appName;
        AppVersion = "0.1";
        AppDescription = string.Empty;
    }
    
}