using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;

namespace OilCore.Models.Base;

public abstract class AssetModel : BaseEntity
{
    protected AssetModel()
    {
    }
    
    protected AssetModel(BaseAsset model, string assetNumber)
    {
        Model = model;
        if (string.IsNullOrEmpty(assetNumber))
        {
            throw new ArgumentException("Asset number cannot be null or empty.", nameof(assetNumber));
        }
        AssetNumber = assetNumber;
        Status = AssetStatus.Active; // Default status for a new asset is Active
    }
    
    protected AssetModel(BaseAsset model, string assetNumber, AssetStatus status)
    {
        Model = model;
        if (string.IsNullOrEmpty(assetNumber))
        {
            throw new ArgumentException("Asset number cannot be null or empty.", nameof(assetNumber));
        }
        AssetNumber = assetNumber;
        Status = status;
    }
    public BaseAsset Model { get; set; } = new();
   
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(15)]
    public string AssetNumber { get; set; } = string.Empty;

    [MaxLength(15)]
    public string SerialNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    public AssetStatus Status { get; set; } = AssetStatus.Unknown;
}