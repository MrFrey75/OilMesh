namespace OilCore.Models;

public class FacultyUser : User
{
    public virtual Department Department { get; set; } = new();
    public virtual List<Course> Courses { get; set; } = new();
}