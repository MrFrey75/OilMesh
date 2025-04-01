using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;

namespace OilCore.Models.Base;

public abstract class BaseAssetModel : BaseEntity
{
    protected BaseAssetModel() { }

    protected BaseAssetModel(string modelName, string modelNumber)
    {
        ModelName = modelName;
        ModelNumber = modelNumber;
    }

    protected BaseAssetModel(string modelName, string modelNumber, Manufacturer manufacturer)
        : this(modelName, modelNumber)
    {
        Manufacturer = manufacturer;
    }

    protected BaseAssetModel(string modelName, string modelNumber, Manufacturer manufacturer, AssetType type)
        : this(modelName, modelNumber, manufacturer)
    {
        Type = type;
    }

    [MaxLength(50)]
    public string ModelName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string ModelNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public Manufacturer Manufacturer { get; set; } = new();
    public AssetType Type { get; set; } = AssetType.Unknown;
    public List<ConnectionType> Connections { get; set; } = [];
    public List<UsbType> UsbTypes { get; set; } = [];

    public bool IsWifiCapable => Connections.Contains(ConnectionType.WiFi);
    public bool IsBluetoothCapable => Connections.Contains(ConnectionType.Bluetooth);
    public bool IsEthernetCapable => Connections.Contains(ConnectionType.Ethernet);

    public override string ToString() =>
        $"{Manufacturer.Abbreviation} - {ModelName} ({ModelNumber})";
}