using OilCore.Data;
using OilCore.Enumerations;
using OilCore.Interfaces;
using OilCore.Models;

namespace OilCore.Services;

public class AssetService : IDisposable
{
    private readonly IOilLoggerService<AddressService> _logger;
    private readonly OilCoreDbContext _context;

    public AssetService(IOilLoggerService<AddressService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("AssetService initialized");
    }

    public void Dispose()
    {
        _logger.LogInfo("Disposing AssetService");
        _context?.Dispose();
    }

    public List<Asset> GetAssets(AssetType? assetType = null) =>
        _context.Assets
            .Where(x => x.DeletedAt == null && (assetType == null || x.Model.Type == assetType))
            .ToList();

    public Asset GetAsset(Guid uid) =>
        _context.Assets.Find(uid) ?? throw new Exception("Asset not found");

    public void AddAsset(Asset asset)
    {
        _context.Assets.Add(asset);
        _context.SaveChanges();
    }

    public void UpdateAsset(Asset asset)
    {
        _context.Assets.Update(asset);
        _context.SaveChanges();
    }

    public void DeleteAsset(Asset asset)
    {
        var found = _context.Assets.Find(asset) ?? throw new Exception("Asset not found");
        found.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public async Task<Asset?> GetAssetAsync(Guid uid) =>
        await Task.Run(() => GetAsset(uid));

    public async Task AddAssetAsync(Asset asset) =>
        await Task.Run(() => AddAsset(asset));

    public async Task UpdateAssetAsync(Asset asset) =>
        await Task.Run(() => UpdateAsset(asset));

    public async Task DeleteAssetAsync(Asset asset) =>
        await Task.Run(() => DeleteAsset(asset));
}