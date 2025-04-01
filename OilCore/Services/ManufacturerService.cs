using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class ManufacturerService : IDisposable
{
    private readonly IOilLoggerService<ManufacturerService> _logger;
    private readonly OilCoreDbContext _context;

    public ManufacturerService(IOilLoggerService<ManufacturerService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("ManufacturerService initialized");
    }

    public void Dispose()
    {
        _logger.LogInfo("Disposing ManufacturerService");
        _context?.Dispose();
    }

    public List<Manufacturer> GetManufacturers() =>
        _context.Manufacturers.Where(x => x.DeletedAt == null).ToList();

    public Manufacturer GetManufacturer(Guid uid) =>
        _context.Manufacturers.Find(uid) ?? throw new Exception("Manufacturer not found");

    public Manufacturer GetManufacturer(string name) =>
        _context.Manufacturers.FirstOrDefault(x => x.Name == name)
        ?? throw new Exception("Manufacturer not found");

    public Manufacturer AddManufacturer(Manufacturer model)
    {
        _context.Manufacturers.Add(model);
        _context.SaveChanges();
        return model;
    }

    public Manufacturer UpdateManufacturer(Manufacturer model)
    {
        _context.Manufacturers.Update(model);
        _context.SaveChanges();
        return model;
    }

    public void DeleteManufacturer(Manufacturer model)
    {
        var found = _context.Manufacturers.Find(model) ?? throw new Exception("Manufacturer not found");
        found.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public async Task<Manufacturer> AddManufacturerAsync(Manufacturer model) =>
        await Task.Run(() => AddManufacturer(model));

    public async Task<Manufacturer> UpdateManufacturerAsync(Manufacturer model) =>
        await Task.Run(() => UpdateManufacturer(model));

    public async Task DeleteManufacturerAsync(Manufacturer model) =>
        await Task.Run(() => DeleteManufacturer(model));
}