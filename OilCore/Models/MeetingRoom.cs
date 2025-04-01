using System.ComponentModel.DataAnnotations;

namespace OilCore.Models;

public class MeetingRoom : Room
{
    public MeetingRoom()
        : base() // Default constructor for ORM frameworks like Entity Framework.
    {
        // Default constructor for ORM frameworks like Entity Framework.
    }
    
    public MeetingRoom(string number)
        : base(number)
    {
    }
    public MeetingRoom(string number, PhoneNumber phoneNumber)
        : base(number, phoneNumber)
    {
    }
    
    [MaxLength(50)]
    public string RoomName { get; set; } = string.Empty;
}