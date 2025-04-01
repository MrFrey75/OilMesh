using System.ComponentModel.DataAnnotations;

namespace OilCore.Models;

public class AuxiliaryRoom : Room
{
    public AuxiliaryRoom()
        : base() // Default constructor for ORM frameworks like Entity Framework.
    {
        // Default constructor for ORM frameworks like Entity Framework.
    }
    
    public AuxiliaryRoom(string number)
        : base(number)
    {
    }
    
    public AuxiliaryRoom(string number, PhoneNumber phoneNumber)
        : base(number, phoneNumber)
    {
    }
    
    [MaxLength(50)]
    public string RoomName { get; set; } = string.Empty;
}