using OilCore.Enumerations;

namespace OilCore.Models.Base;

public class BaseAsset : BaseAssetModel
{
    // Empty constructor for EF Core
    public BaseAsset() { }

    public BaseAsset(string modelName, string modelNumber)
        : base(modelName, modelNumber) { }

    public BaseAsset(string modelName, string modelNumber, Manufacturer manufacturer)
        : base(modelName, modelNumber, manufacturer) { }

    public BaseAsset(string modelName, string modelNumber, Manufacturer manufacturer, AssetType type)
        : base(modelName, modelNumber, manufacturer, type) { }
}