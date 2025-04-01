using System.ComponentModel.DataAnnotations;

namespace OilCore.Models;

public sealed class Office : Room
{
    public Office()
        : base() // Default constructor for ORM frameworks like Entity Framework.
    {
        // Default constructor for ORM frameworks like Entity Framework.
    }
    
    public Office(string number)
        : base(number)
    {
    }
    public Office(string number, PhoneNumber phoneNumber)
        : base(number, phoneNumber)
    {
    }
    
    [MaxLength(50)]
    public string OfficeName { get; set; } = string.Empty;

    public List<AdministratorUser> Administrators { get; set; } = [];
}