using System.ComponentModel.DataAnnotations;
using OilCore.Services;

namespace OilCore.Models.Base;

public abstract class UserCredentialModel : BaseEntity
{
    protected UserCredentialModel()
    {
        // Default constructor for EF Core
    }
    protected UserCredentialModel(User user, string password)
    {
        User = user;
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        var pass  = EncryptionService.HashPassword(password);
        PasswordHash = pass.hash; // Set the hashed password
        PasswordSalt = pass.salt; // Set the salt used for hashing
    }
    
    public required User User { get; set; }
    [MaxLength(100)]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(50)]
    public string PasswordSalt { get; set; } = string.Empty;
    
    public void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentException("Password cannot be null or empty.", nameof(password));

        var pass  = EncryptionService.HashPassword(password);
        PasswordHash = pass.hash; // Set the hashed password
        PasswordSalt = pass.salt; // Set the salt used for hashing
    }
    
}