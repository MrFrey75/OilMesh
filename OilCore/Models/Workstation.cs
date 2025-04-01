using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public sealed class Workstation : Asset
{
    public Workstation() : base(new BaseAsset(), string.Empty) { }

    public Workstation(BaseAsset model, string assetNumber)
        : base(model, assetNumber) { }

    public Workstation(BaseAsset model, string assetNumber, AssetStatus status)
        : base(model, assetNumber, status) { }

    public WorkstationType WorkstationType { get; set; } = WorkstationType.Unknown;
    public List<Peripheral> Peripherals { get; set; } = [];
}