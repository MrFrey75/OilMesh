using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class EmailAddressService
{
    private readonly IOilLoggerService<EmailAddressService> _logger;
    private readonly OilCoreDbContext _context;

    public EmailAddressService(OilCoreDbContext context, IOilLoggerService<EmailAddressService> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogInfo("EmailAddressService initialized");
    }
    
    public void Dispose()
    {
        // Dispose of the context if needed
        _logger.LogInfo("Disposing EmailAddressService");
        _context?.Dispose();
    }
    
    public List<EmailAddress> GetEmailAddresses()
    {
        _logger.LogInfo("Getting email addresses");
        return _context.EmailAddresses.Where(x => x.DeletedAt == null).ToList();
    }
    
    public EmailAddress GetEmailAddress(Guid uid)
    {
        _logger.LogInfo("Getting email address by ID");
        return _context.EmailAddresses.Find(uid) ?? throw new Exception("Email address not found");
    }
    
    public EmailAddress AddEmailAddress(EmailAddress emailAddress)
    {
        _logger.LogInfo("Adding email address");
        _context.EmailAddresses.Add(emailAddress);
        _context.SaveChanges();
        return emailAddress;
    }
    
    public EmailAddress UpdateEmailAddress(EmailAddress emailAddress)
    {
        _logger.LogInfo("Updating email address");
        _context.EmailAddresses.Update(emailAddress);
        _context.SaveChanges();
        return emailAddress;
    }
    
    public void DeleteEmailAddress(EmailAddress emailAddress)
    {
        _logger.LogInfo("Deleting email address");
        emailAddress = _context.EmailAddresses.Find(emailAddress) ?? throw new Exception("Email address not found");
        emailAddress.DeletedAt = DateTime.UtcNow;
        _context.EmailAddresses.Update(emailAddress);
        _context.SaveChanges();
    }
    
    public async Task<EmailAddress> AddEmailAddressAsync(EmailAddress emailAddress)
    {
        _logger.LogInfo("Adding email address asynchronously");
        await _context.EmailAddresses.AddAsync(emailAddress);
        await _context.SaveChangesAsync();
        return emailAddress;
    }
    
    public async Task<EmailAddress> UpdateEmailAddressAsync(EmailAddress emailAddress)
    {
        _logger.LogInfo("Updating email address asynchronously");
        _context.EmailAddresses.Update(emailAddress);
        await _context.SaveChangesAsync();
        return emailAddress; 
    }
    
    public async Task DeleteEmailAddressAsync(EmailAddress emailAddress)
    {
        _logger.LogInfo("Deleting email address asynchronously");
        emailAddress = await _context.EmailAddresses.FindAsync(emailAddress) ?? throw new Exception("Email address not found");
        emailAddress.DeletedAt = DateTime.UtcNow;
        _context.EmailAddresses.Update(emailAddress);
        await _context.SaveChangesAsync();
    }
    
}