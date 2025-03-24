namespace OilCore.Interfaces;

public class IInitializationService
{
    private readonly IOilLoggerService logger;
    public IInitializationService(IOilLoggerService logger)
    {
        this.logger = logger;
        logger.LogInfo("InitializationService initialized");
    }
    
    private void SeedSchools()
    {
        logger.LogInfo("Seeding schools");
    }
    
    
}