namespace OilCore.Models;

public class StudentUser : User
{
    public StudentUser() {}
    public StudentUser(string username) : base(username) {}

    public int GraduationYear { get; set; }
    public virtual School SendingSchool { get; set; } = new();
    public virtual List<Course> Courses { get; set; } = new();
}