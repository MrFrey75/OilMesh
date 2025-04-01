using System.ComponentModel.DataAnnotations;

namespace OilCore.Models.Base;

public abstract class CourseModel : BaseEntity
{
    protected CourseModel()
    {
    }
    
    protected CourseModel(string name, string code)
    {
        Name = name;
        Code = code;
    }
    
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    public Department Department { get; set; } = new();

    public List<FacultyUser> FacultyMembers { get; set; } = [];
    public List<StudentUser> Students { get; set; } = [];
    public List<Classroom> Classrooms { get; set; } = [];
}