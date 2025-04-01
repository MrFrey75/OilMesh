using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class PersonService : IDisposable
{
    private readonly IOilLoggerService<PersonService> _logger;
    private readonly OilCoreDbContext _context;

    public PersonService(IOilLoggerService<PersonService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("PersonService initialized");
    }

    public void Dispose()
    {
        _logger.LogInfo("Disposing PersonService");
        _context?.Dispose();
    }

    private T GetEntity<T>(Guid uid) where T : class => _context.Set<T>().Find(uid) ?? throw new("Entity not found");
    private void SoftDelete<T>(T entity) where T : class, IDeletable { entity.DeletedAt = DateTime.UtcNow; _context.Update(entity); _context.SaveChanges(); }
    private async Task SoftDeleteAsync<T>(T entity) where T : class, IDeletable { entity.DeletedAt = DateTime.UtcNow; _context.Update(entity); await _context.SaveChangesAsync(); }

    public List<Person> GetPeople() => _context.People.Where(p => p.DeletedAt == null).ToList();
    public Person GetPerson(Guid uid) => GetEntity<Person>(uid);
    public Person AddPerson(Person p) { _context.People.Add(p); _context.SaveChanges(); return p; }
    public Person UpdatePerson(Person p) { _context.People.Update(p); _context.SaveChanges(); return p; }
    public void DeletePerson(Person p) => SoftDelete(p);

    public List<User> GetUsers() => _context.Users.Where(x => x.DeletedAt == null).ToList();
    public User GetUser(string username) => _context.Users.FirstOrDefault(u => u.Username.ToUpper() == username.ToUpper()) ?? throw new("User not found");

    public List<StudentUser> GetStudentUsers() => _context.StudentUsers.Where(x => x.DeletedAt == null).ToList();
    public List<FacultyUser> GetFacultyUsers() => _context.FacultyUsers.Where(x => x.DeletedAt == null).ToList();
    public List<AdministratorUser> GetAdministratorUsers() => _context.AdministratorUsers.Where(x => x.DeletedAt == null).ToList();
}

public interface IDeletable { DateTime? DeletedAt { get; set; } }