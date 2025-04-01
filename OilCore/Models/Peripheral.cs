using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public class Peripheral : Asset
{
    public Peripheral() : base(new BaseAsset(), string.Empty) { }

    public Peripheral(BaseAsset model, string assetNumber)
        : base(model, assetNumber) { }

    public Peripheral(BaseAsset model, string assetNumber, AssetStatus status)
        : base(model, assetNumber, status) { }

    public PeripheralType PeripheralType { get; set; } = PeripheralType.Unknown;
}