using System.ComponentModel.DataAnnotations;

namespace OilCore.Models;

public class AdministratorUser : User
{
    public AdministratorUser() {}

    [MaxLength(50)] public string Title { get; set; } = string.Empty;
    [MaxLength(50)] public string Position { get; set; } = string.Empty;
    public virtual List<Office> Offices { get; set; } = new();
}