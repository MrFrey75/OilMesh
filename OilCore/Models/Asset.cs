using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public class Asset : AssetModel
{
    public Asset() : base(new BaseAsset(), string.Empty) { }

    public Asset(BaseAsset model, string assetNumber)
        : base(model, assetNumber) { }

    public Asset(BaseAsset model, string assetNumber, AssetStatus status)
        : base(model, assetNumber, status) { }
}