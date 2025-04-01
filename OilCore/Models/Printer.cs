using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public class Printer : Asset
{
    public Printer() : base(new BaseAsset(), string.Empty) { }

    public Printer(BaseAsset model, string assetNumber)
        : base(model, assetNumber) { }

    public Printer(BaseAsset model, string assetNumber, AssetStatus status)
        : base(model, assetNumber, status) { }

    public PrinterType PrinterType { get; set; } = PrinterType.Unknown;
    [MaxLength(15)]
    public string SecurityPin { get; set; } = string.Empty;
}