using System.ComponentModel.DataAnnotations;

namespace OilCore.Models;

public abstract class BaseLookup
{
    protected BaseLookup()
    {
        Oid = 0;
        Name = string.Empty;
        Code = string.Empty;
        Description = string.Empty;
        SortOrder = 0;
    }
    
    protected BaseLookup(string name, string code)
    {
        Oid = 0; // Default value, will be set by the database
        Name = name;
        Code = code.ToUpperInvariant(); // Ensure code is always in uppercase
        Description = string.Empty;
        SortOrder = 0;
    }

    [Key]
    public int Oid { get; init; } // Auto-incremented

    [MaxLength(50)]
    public string Name { get; set; }

    private string _code = string.Empty;

    [MaxLength(10)]
    public string Code
    {
        get => _code;
        set => _code = value?.ToUpperInvariant() ?? string.Empty;
    }

    [MaxLength(100)]
    public string Description { get; set; }

    public DateTime? DeletedAt { get; set; }
    public int SortOrder { get; set; }

    public override string ToString() => Name;
    public string ToCode() => Code;
}