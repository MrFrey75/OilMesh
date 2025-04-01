using Google.Apis.Auth.OAuth2;
using OilCore.Data;
using OilCore.Interfaces;
using OilCore.Models;
using UserCredential = OilCore.Models.UserCredential;

namespace OilCore.Services;

public class UserCredentialService
{
    private readonly IOilLoggerService<UserCredentialService> _logger;
    private readonly OilCoreDbContext _context;

    public UserCredentialService(IOilLoggerService<UserCredentialService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("UserCredentialService initialized");
    }

    public List<UserCredential> GetUserCredentials(User user) => _context.UserCredentials.Where(c => c.User.Uid == user.Uid).ToList();

    public bool ValidateCredential(User user, string password)
    {
        _logger.LogInfo("Validating credential");
        return EncryptionService.VerifyCredential(user, password);
    }

    public UserCredential GetCurrentCredential(User user) => _context.UserCredentials
        .Where(u => u.User.Uid == user.Uid)
        .OrderByDescending(c => c.CreatedAt)
        .FirstOrDefault() ?? throw new("Credential not found");

    public UserCredential AddUserCredential(UserCredential cred)
    {
        _context.UserCredentials.Add(cred); _context.SaveChanges(); return cred;
    }

    public void DeleteUserCredential(UserCredential cred)
    {
        var target = _context.UserCredentials.Find(cred.Uid) ?? throw new("Credential not found");
        target.DeletedAt = DateTime.UtcNow;
        _context.UserCredentials.Update(target);
        _context.SaveChanges();
    }
}