using Microsoft.EntityFrameworkCore;
using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class PhoneNumberService
{
    private readonly IOilLoggerService<PhoneNumberService> _logger;
    private readonly OilCoreDbContext _context;
    
    public PhoneNumberService(IOilLoggerService<PhoneNumberService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("PhoneNumberService initialized");
    }
    
    public void Dispose()
    {
        // Dispose of the context if needed
        _logger.LogInfo("Disposing PhoneNumberService");
        _context?.Dispose();
    }
    
    public List<PhoneNumber> GetPhoneNumbers()
    {
        _logger.LogInfo("Getting phone numbers");
        return _context.PhoneNumbers.Where(x => x.DeletedAt == null).ToList();
    }
    
    public PhoneNumber GetPhoneNumber(Guid uid)
    {
        _logger.LogInfo("Getting phone number by ID");
        return _context.PhoneNumbers.Find(uid) ?? throw new Exception("Phone number not found");
    }
    
    public PhoneNumber AddPhoneNumber(PhoneNumber phoneNumber)
    {
        _logger.LogInfo("Adding phone number");
        _context.PhoneNumbers.Add(phoneNumber);
        _context.SaveChanges();
        return phoneNumber;
    }
    
    public PhoneNumber UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        _logger.LogInfo("Updating phone number");
        _context.PhoneNumbers.Update(phoneNumber);
        _context.SaveChanges();
        return phoneNumber;
    }
    
    public void DeletePhoneNumber(PhoneNumber phoneNumber)
    {
        _logger.LogInfo("Deleting phone number");
        phoneNumber = _context.PhoneNumbers.Find(phoneNumber) ?? throw new Exception("Phone number not found");
        phoneNumber.DeletedAt = DateTime.UtcNow;
        _context.PhoneNumbers.Update(phoneNumber);
        _context.SaveChanges();
    }
    
    public async Task<PhoneNumber> GetPhoneNumberAsync(Guid uid)
    {
        _logger.LogInfo("Getting phone number by ID asynchronously");
        var phoneNumber = await _context.PhoneNumbers.FindAsync(uid);
        if (phoneNumber == null)
        {
            throw new Exception("Phone number not found");
        }
        return phoneNumber;
    }
    
    public async Task<List<PhoneNumber>> GetPhoneNumbersAsync()
    {
        _logger.LogInfo("Getting phone numbers asynchronously");
        return await _context.PhoneNumbers
            .Where(x => x.DeletedAt == null)
            .ToListAsync();
    }
    
    public async Task<PhoneNumber> AddPhoneNumberAsync(PhoneNumber phoneNumber)
    {
        _logger.LogInfo("Adding phone number asynchronously");
        await _context.PhoneNumbers.AddAsync(phoneNumber);
        await _context.SaveChangesAsync();
        return phoneNumber;
    }
    
    public async Task<PhoneNumber> UpdatePhoneNumberAsync(PhoneNumber phoneNumber)
    {
        _logger.LogInfo("Updating phone number asynchronously");
        _context.PhoneNumbers.Update(phoneNumber);
        await _context.SaveChangesAsync();
        return phoneNumber;
    }
    
    public async Task DeletePhoneNumberAsync(PhoneNumber phoneNumber)
    {
        _logger.LogInfo("Deleting phone number asynchronously");
        phoneNumber = await _context.PhoneNumbers.FindAsync(phoneNumber) ?? throw new Exception("Phone number not found");
        phoneNumber.DeletedAt = DateTime.UtcNow; // Soft delete
        _context.PhoneNumbers.Update(phoneNumber);
        await _context.SaveChangesAsync();
    }
    
}