using System.ComponentModel.DataAnnotations;
using OilCore.Enumerations;

namespace OilCore.Models.Base;

public abstract class SchoolModel : BaseEntity
{
    protected SchoolModel()
    {
        // Default constructor for EF Core
    }
    
    protected SchoolModel(string name)
    {
        Name = name;
    }
    
    protected SchoolModel(string name, string city, UsState state)
    {
        Name = name;
        Address.City = city;
        Address.State = state;
    }
    
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public Address Address { get; set; } = new();
    public List<PhoneNumber> PhoneNumbers { get; set; } = [];
    public bool IsDefault { get; set; }
}