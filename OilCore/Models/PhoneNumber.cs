using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public class PhoneNumber : BaseEntity
{
    public PhoneNumber()
    {
        // Default constructor for EF Core
    }

    protected PhoneNumber(string phoneNumber)
    {
        Type = PhoneType.Unknown;
        if (phoneNumber.Length != 10) return;
        AreaCode = phoneNumber[..3];
        MainNumber = phoneNumber.Substring(3, 7);
    }

    protected PhoneNumber(PhoneType type, string areaCode, string phoneNumber, string extension = "")
    {
        AreaCode = areaCode;
        MainNumber = phoneNumber;
        Extension = extension;
        Type = type;
    }
    public PhoneType Type { get; set; } = PhoneType.Unknown;

    [MaxLength(3)]
    public string AreaCode { get; set; } = string.Empty; // Area code (3 digits), e.g., "123"
    [MaxLength(7)]
    public string MainNumber { get; set; } = string.Empty; // Phone number (7 digits), e.g., "4567890"
    [MaxLength(6)]
    public string Extension { get; set; } = string.Empty; // Optional extension (up to 6 characters), e.g., "1234"
    
    public string FullNumber => $"{AreaCode}-{MainNumber}{(string.IsNullOrWhiteSpace(Extension) ? "" : $" x{Extension}")}";
    
    public string DisplayNumber => $"{AreaCode}-{MainNumber}";

}