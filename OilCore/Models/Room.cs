using OilCore.Models.Base;

namespace OilCore.Models;

public class Room : RoomModel
{
    // This class is intentionally left empty to allow polymorphism in the DbContext.
    // This allows the RoomModel to be used in the DbSet without issues.
    public Room()
        : base() // Default constructor for ORM frameworks like Entity Framework
    {
    }

    public Room(string number)
        : base(number)
    {
    }

    public Room(string number, PhoneNumber phoneNumber)
        : base(number, phoneNumber)
    {
    }
}