using OilCore.Enumerations;
using OilCore.Models.Base;

namespace OilCore.Models;

public class School : SchoolModel
{
    // This class is intentionally left empty to allow polymorphism in the DbContext.
    // This allows the SchoolModel to be used in the DbSet without issues.
    public School()
        : base() // Default constructor for EF Core
    {
    }

    public School(string name)
        : base(name)
    {
    }

    public School(string name, string city, UsState state)
        : base(name, city, state)
    {
    }
}