using Microsoft.EntityFrameworkCore;
using OilCore.Data;
using OilCore.Enumerations;
using OilCore.Interfaces;
using OilCore.Models;
using OilCore.Models.Base;

namespace OilCore.Services;

public class BaseAssetService : IDisposable
{
    private readonly IOilLoggerService<BaseAssetService> _logger;
    private readonly OilCoreDbContext _context;

    public BaseAssetService(IOilLoggerService<BaseAssetService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("BaseAssetService initialized");
    }

    public void Dispose()
    {
        _logger.LogInfo("Disposing BaseAssetService");
        _context?.Dispose();
    }

    public List<BaseAsset> GetBaseAssets(AssetType? assetType = null) =>
        _context.BaseAssets
            .Where(x => x.DeletedAt == null && (assetType == null || x.Type == assetType))
            .ToList();

    public BaseAsset GetBaseAsset(Guid uid) =>
        _context.BaseAssets.Find(uid) ?? throw new Exception("Base asset not found");

    public void AddBaseAsset(BaseAsset baseAsset)
    {
        _context.BaseAssets.Add(baseAsset);
        _context.SaveChanges();
    }

    public void UpdateBaseAsset(BaseAsset baseAsset)
    {
        _context.BaseAssets.Update(baseAsset);
        _context.SaveChanges();
    }

    public void DeleteBaseAsset(BaseAsset baseAsset)
    {
        var found = _context.BaseAssets.Find(baseAsset) ?? throw new Exception("Base asset not found");
        found.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public async Task<BaseAsset> GetBaseAssetAsync(Guid uid) =>
        await Task.Run(() => GetBaseAsset(uid));

    public async Task AddBaseAssetAsync(BaseAsset baseAsset) =>
        await Task.Run(() => AddBaseAsset(baseAsset));

    public async Task UpdateBaseAssetAsync(BaseAsset baseAsset) =>
        await Task.Run(() => UpdateBaseAsset(baseAsset));

    public async Task DeleteBaseAssetAsync(BaseAsset baseAsset) =>
        await Task.Run(() => DeleteBaseAsset(baseAsset));
}