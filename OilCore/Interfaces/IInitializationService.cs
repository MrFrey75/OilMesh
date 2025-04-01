using OilCore.Services;

namespace OilCore.Interfaces;

public class IInitializationService
{
    private readonly IOilLoggerService<IInitializationService> logger;
    public IInitializationService(IOilLoggerService<IInitializationService> logger)
    {
        this.logger = logger;
        logger.LogInfo("InitializationService initialized");
    }
    
    private void SeedSchools()
    {
        logger.LogInfo("Seeding schools");
    }
    
    
}