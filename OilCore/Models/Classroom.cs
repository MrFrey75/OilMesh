using OilCore.Models.Base;

namespace OilCore.Models;

public sealed class Classroom : Room
{
    public Classroom() 
        : base() // Default constructor for ORM frameworks like Entity Framework.
    {
        // Default constructor for ORM frameworks like Entity Framework.
    }
    public Classroom(string number)
        : base(number)
    {
    }
    public Classroom(string number, PhoneNumber phoneNumber)
        : base(number, phoneNumber)
    {
    }
    public bool Primary { get; set; }
    public List<CourseModel> Courses { get; set; } = [];
}