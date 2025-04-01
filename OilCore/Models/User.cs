using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OilCore.Models.Base;
using OilCore.Services;

namespace OilCore.Models;

public class User : Person, IDeletable
{
    protected User() {}
    protected User(string username) => Username = username;
    public User(string firstName, string lastName, string username) : base(firstName, lastName)
    {
        Username = string.IsNullOrWhiteSpace(username) || username.Length > 20
            ? throw new ArgumentException("Invalid username", nameof(username))
            : username;
    }

    [MaxLength(20)] public string Username { get; set; } = string.Empty;

    public virtual List<UserCredentialModel> Credentials { get; protected set; } = new();
    [NotMapped] public UserCredentialModel? CurrentCredential => Credentials.OrderByDescending(c => c.CreatedAt).FirstOrDefault();

    public void InitCredentials(string password)
    {
        if (Credentials.Count > 0 || string.IsNullOrEmpty(password)) return;

        var credential = new UserCredential(this, password)
        {
            User = null
        };
        Credentials.Add(credential);
    }
}