using System.ComponentModel.DataAnnotations;

namespace OilCore.Models.Base;

public abstract class RoomModel : BaseEntity
{
    protected RoomModel()
    {
        // Default constructor for ORM frameworks like Entity Framework.
    }
    
    protected RoomModel(string number)
    {
        Number = number;
    }
    
    protected RoomModel(string number, PhoneNumber phoneNumber)
    {
        Number = number;
        PhoneNumber = phoneNumber;
    }
    
    [MaxLength(3)]
    public string Number { get; init; } = "000";
    public virtual List<Asset> Assets { get; set; } = [];
    public PhoneNumber PhoneNumber { get; set; } = new();
    public School School { get; set; } = new();
}