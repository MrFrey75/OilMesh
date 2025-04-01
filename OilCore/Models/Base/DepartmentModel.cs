using System.ComponentModel.DataAnnotations;

namespace OilCore.Models.Base;

public abstract class DepartmentModel : BaseEntity
{
    protected DepartmentModel()
    {

    }

    protected DepartmentModel(string name, string code)
    {
        Name = name;
        Code = code;
    }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;
    public List<Course> Courses { get; set; } = [];
 
}