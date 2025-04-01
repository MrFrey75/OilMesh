using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OilCore.Enumerations;

namespace OilCore.Models.Base;

public abstract class PersonModel : BaseEntity
{
    protected PersonModel() {}

    protected PersonModel(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    [MaxLength(50)] public string FirstName { get; protected set; } = string.Empty;
    [MaxLength(50)] public string LastName { get; protected set; } = string.Empty;

    public virtual List<Address> Addresses { get; protected set; } = new();
    public virtual List<EmailAddress> EmailAddresses { get; protected set; } = new();
    public virtual List<PhoneNumber> PhoneNumbers { get; protected set; } = new();

    public DateOnly DateOfBirth { get; set; }

    [MaxLength(500)] public string Notes { get; set; } = string.Empty;

    [NotMapped] public string FullName => $"{FirstName} {LastName}".Trim();
}