using OilCore.Data;
using OilCore.Interfaces;
using OilCore.Models;

namespace OilCore.Services;

public class InitializationService //: IInitializationService
{
    private readonly IOilLoggerService<InitializationService> _logger;
    private readonly OilCoreDbContext _context;
    public InitializationService(IOilLoggerService<InitializationService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("InitializationService initialized");
    }
    
    public void SeedRooms()
    {

    }

    public void SeedDepartments()
    {
        
    }
    
    public void SeedSchools()
    {

        
    }
}