using System.ComponentModel.DataAnnotations;

namespace OilCore.Models.Base;

public abstract class ManufacturerModel : BaseEntity
{
    protected ManufacturerModel()
    {
        // Default constructor for EF Core
    }

    protected ManufacturerModel(string name, string website)
    {
        Name = name;
        Website = website;
    }
    
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Website { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Abbreviation { get; set; } = string.Empty;
    
    public List<Address> Locations { get; set; } = []; // This can be used to store locations of the manufacturer if needed
    public List<PhoneNumber> PhoneNumbers { get; set; } = []; // Manufacturer's phone numbers
    public List<EmailAddress> EmailAddresses { get; set; } = []; // Manufacturer's email addresses
    
    
    
}