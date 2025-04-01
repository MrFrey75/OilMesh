using System.ComponentModel.DataAnnotations;

namespace OilCore.Models;

public class GpsLocation : Location
{
    [MaxLength(100)]
    public string SourceDevice { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public bool IsLive { get; set; } = false;

    public float AccuracyMeters { get; set; } = 0.0f;

    public float Altitude { get; set; } = 0.0f;

    public string GetGeoSummary() =>
        $"Lat: {Latitude}, Lon: {Longitude}, Alt: {Altitude}m, Accuracy: ±{AccuracyMeters}m, Timestamp: {Timestamp:u}";

    public override string ToString() =>
        $"{SourceDevice} @ {Latitude}, {Longitude} (±{AccuracyMeters}m)";
}