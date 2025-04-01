using System.ComponentModel.DataAnnotations;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace OilCore.Models.Base;

// ---- Base Classes ----
public abstract class BaseEntity
{
    [Key]
    public Guid Uid { get; init; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    private DateTime _updatedAt = DateTime.UtcNow;
    public DateTime UpdatedAt
    {
        get => _updatedAt;
        set => _updatedAt = value < CreatedAt ? CreatedAt : value;
    }

    public DateTime? DeletedAt { get; set; }
}