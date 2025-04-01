using OilCore.Models.Base;

namespace OilCore.Models;

public sealed class Course : CourseModel
{
    // This class is intentionally left empty to allow polymorphism in the DbContext.
    // This allows the CourseModel to be used in the DbSet without issues.
    public Course()
    {
    }

    public Course(string name, string code) : base(name, code)
    {
    }
}