using Microsoft.EntityFrameworkCore;
using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class AddressService : IDisposable
{
    private readonly IOilLoggerService<AddressService> _logger;
    private readonly OilCoreDbContext _context;

    public AddressService(IOilLoggerService<AddressService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("AddressService initialized");
    }

    public void Dispose()
    {
        _logger.LogInfo("Disposing AddressService");
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public List<Address> GetAddresses() =>
        _context.Addresses
            .Where(x => x.DeletedAt == null)
            .ToList();

    public Address GetAddressById(int id) =>
        _context.Addresses.Find(id) ?? throw new Exception("Address not found");

    public List<Address> GetPrimaryAddresses() =>
        _context.Addresses
            .Where(x => x.DeletedAt == null && x.IsPrimary)
            .ToList();

    public List<Address> GetMailingAddresses() =>
        _context.Addresses
            .Where(x => x.DeletedAt == null && x.IsMailingAddress)
            .ToList();

    public List<Address> GetBillingAddresses() =>
        _context.Addresses
            .Where(x => x.DeletedAt == null && x.IsBillingAddress)
            .ToList();

    public void AddAddress(Address address)
    {
        _context.Addresses.Add(address);
        _context.SaveChanges();
    }

    public void UpdateAddress(Address address)
    {
        _context.Addresses.Update(address);
        _context.SaveChanges();
    }

    public void DeleteAddress(Address address)
    {
        var found = _context.Addresses.Find(address) ?? throw new Exception("Address not found");
        found.DeletedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }

    public async Task<List<Address>> GetAddressesAsync() =>
        await _context.Addresses
            .Where(x => x.DeletedAt == null)
            .ToListAsync();

    public async Task<Address> GetAddressByIdAsync(int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        return address ?? throw new Exception("Address not found");
    }

    public async Task AddAddressAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAddressAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(Address address)
    {
        var found = await _context.Addresses.FindAsync(address);
        if (found == null)
            throw new Exception("Address not found");

        found.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}
