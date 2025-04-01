using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public abstract class EmailAddress : BaseEntity
{
    protected EmailAddress()
    {
        // Default constructor for EF Core
    }
    protected EmailAddress(EmailType type, string address)
    {
        Type = type;
        Address = address;
    }
    public EmailType Type { get; set; } = EmailType.Unknown;

    [MaxLength(50)]
    public string Address { get; set; } = string.Empty;
}