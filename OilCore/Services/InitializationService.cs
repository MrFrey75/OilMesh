using OilCore.Data;
using OilCore.Interfaces;
using OilCore.Models;

namespace OilCore.Services;

public class InitializationService //: IInitializationService
{
    private readonly IOilLoggerService _logger;
    private readonly CoreDbContext _context;
    public InitializationService(IOilLoggerService logger, CoreDbContext context)
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