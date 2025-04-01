using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class SchoolService
{
    private IOilLoggerService<SchoolService> _logger;
    private OilCoreDbContext _context;

    public SchoolService(IOilLoggerService<SchoolService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("SchoolService initialized");
    }
    
    public void Dispose()
    {
        // Dispose of the context if needed
        _logger.LogInfo("Disposing SchoolService");
        _context?.Dispose();
    }

    public List<School> GetSchools()
    {
        _logger.LogInfo("Getting schools");
        return _context.Schools.Where(x => x.DeletedAt == null).ToList();
    }

    public School GetSchool(Guid uid)
    {
        _logger.LogInfo("Getting school by ID");
        return _context.Schools.Find(uid) ?? throw new Exception("School not found");
    }

    public School GetSchool(string name)
    {
        _logger.LogInfo("Getting school by name");
        return _context.Schools.FirstOrDefault(x => x.Name == name) ?? throw new Exception("School not found");
    }

    public School AddSchool(School school)
    {
        _logger.LogInfo("Adding school");
        _context.Schools.Add(school);
        _context.SaveChanges();
        return school;
    }

    public void AddPhone(School school, PhoneNumber phone)
    {
        _logger.LogInfo("Adding phone to school");
        school = _context.Schools.Find(school) ?? throw new Exception("School not found");
        school.PhoneNumbers.Add(phone);
        UpdateSchool(school);
    }

    public void RemovePhone(School school, PhoneNumber phone)
    {
        _logger.LogInfo("Removing phone from school");
        school = _context.Schools.Find(school) ?? throw new Exception("School not found");
        school.PhoneNumbers.Remove(phone);
        UpdateSchool(school);
    }

    public void SetSchoolAddress(School school, Address address)
    {
        _logger.LogInfo("Setting address for school");
        school = _context.Schools.Find(school) ?? throw new Exception("School not found");
        school.Address = address;
        UpdateSchool(school);
    }

    public School UpdateSchool(School school)
    {
        _logger.LogInfo("Updating school");
        _context.Schools.Update(school);
        _context.SaveChanges();
        return school;
    }
    
    public void DeleteSchool(School school)
    {
        _logger.LogInfo("Deleting school");
        school = _context.Schools.Find(school) ?? throw new Exception("School not found");
        school.DeletedAt = DateTime.UtcNow;
        UpdateSchool(school);
    }

    public async Task<List<School>> GetSchoolsAsync()
    {
        _logger.LogInfo("Getting schools asynchronously");
        return await Task.Run(GetSchools);
    }
    
    public async Task<School> GetSchoolAsync(Guid uid)
    {
        _logger.LogInfo("Getting school by ID asynchronously");
        return await Task.Run(() => GetSchool(uid));
    }
    
    public async Task<School> GetSchoolAsync(string name)
    {
        _logger.LogInfo("Getting school by name asynchronously");
        return await Task.Run(() => GetSchool(name));
    }
    
    public async Task<School> AddSchoolAsync(School school)
    {
        _logger.LogInfo("Adding school asynchronously");
        return await Task.Run(() => AddSchool(school));
        return school;
    }
    
    public async Task<School> UpdateSchoolAsync(School school)
    {
        _logger.LogInfo("Updating school asynchronously");
        return await Task.Run(() => UpdateSchool(school));
    }
    
    public async Task DeleteSchoolAsync(School school)
    {
        _logger.LogInfo("Deleting school asynchronously");
        await Task.Run(() => DeleteSchool(school));
    }
}