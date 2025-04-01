using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public class Monitor : Peripheral
{
    public Monitor() : base(new BaseAsset(), string.Empty) { }

    public Monitor(BaseAsset model, string assetNumber)
        : base(model, assetNumber) { }

    public Monitor(BaseAsset model, string assetNumber, AssetStatus status)
        : base(model, assetNumber, status) { }

    public MonitorType MonitorType { get; set; } = MonitorType.Unknown;
    [MaxLength(10)]
    public string Resolution { get; set; } = string.Empty;
}