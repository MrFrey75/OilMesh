using OilCore.Models.Base;

namespace OilCore.Models;

public class Department : DepartmentModel
{
    // This class is intentionally left empty to allow polymorphism in the DbContext.
    // This allows the DepartmentModel to be used in the DbSet without issues.
    public Department() 
        : base() // Default constructor for EF Core
    {
    }

    public Department(string name, string code) : base(name, code)
    {
    }
}