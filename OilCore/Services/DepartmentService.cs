using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class DepartmentService
{
    private readonly IOilLoggerService<DepartmentService> _logger;
    private readonly OilCoreDbContext _context;
    
    public DepartmentService(IOilLoggerService<DepartmentService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("DepartmentService initialized");
    }
    
    public void Dispose()
    {
        // Dispose of the context if needed
        _logger.LogInfo("Disposing DepartmentService");
        _context?.Dispose();
    }
    
    public List<Department> GetDepartments()
    {
        _logger.LogInfo("Getting departments");
        return _context.Departments.Where(x => x.DeletedAt == null).ToList();
    }
    
    public Department GetDepartment(Guid uid)
    {
        _logger.LogInfo("Getting department by ID");
        var department = _context.Departments.Find(uid) ?? throw new Exception("Department not found");
        department.Courses = _context.Courses.Where(x => x.Department == department).ToList();
        return department;
    }
    
    public Department GetDepartment(string code)
    {
        var department = _context.Departments.FirstOrDefault(x => x.Code.ToUpperInvariant() == code.ToUpperInvariant()) ?? throw new Exception("Department not found");
        department.Courses = _context.Courses.Where(x => x.Department == department).ToList();
        return department;
    }
    
    public void AddDepartment(Department department)
    {
        _logger.LogInfo("Adding department");
        _context.Departments.Add(department);
        _context.SaveChanges();
    }
    
    public void UpdateDepartment(Department department)
    {
        _logger.LogInfo("Updating department");
        _context.Departments.Update(department);
        _context.SaveChanges();
    }
    
    public void DeleteDepartment(Department department)
    {
        _logger.LogInfo("Deleting department");
        department = _context.Departments.Find(department) ?? throw new Exception("Department not found");
        department.DeletedAt = DateTime.UtcNow;
        _context.Departments.Update(department);
        _context.SaveChanges();
    }
    
    public async Task<List<Department>> GetDepartmentsAsync()
    {
        _logger.LogInfo("Getting departments asynchronously");
        return await Task.Run(() => _context.Departments.Where(x => x.DeletedAt == null).ToList());
    }
    
    public async Task<Department> GetDepartmentAsync(Guid uid)
    {
        _logger.LogInfo("Getting department by ID asynchronously");
        return await Task.Run(() => 
        {
            var department = _context.Departments.Find(uid) ?? throw new Exception("Department not found");
            department.Courses = _context.Courses.Where(x => x.Department == department).ToList();
            return department;
        });
    }
    
    public async Task<Department> GetDepartmentAsync(string code)
    {
        _logger.LogInfo("Getting department by code asynchronously");
        return await Task.Run(() => 
        {
            var department = _context.Departments.FirstOrDefault(x => x.Code.ToUpperInvariant() == code.ToUpperInvariant()) 
                             ?? throw new Exception("Department not found");
            department.Courses = _context.Courses.Where(x => x.Department == department).ToList();
            return department;
        });
    }
    
    public async Task AddDepartmentAsync(Department department)
    {
        _logger.LogInfo("Adding department asynchronously");
        await Task.Run(() => 
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        });
    }
    
    public async Task UpdateDepartmentAsync(Department department)
    {
        _logger.LogInfo("Updating department asynchronously");
        await Task.Run(() => 
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        });
    }
    
    public async Task DeleteDepartmentAsync(Department department)
    {
        _logger.LogInfo("Deleting department asynchronously");
        await Task.Run(() => 
        {
            department = _context.Departments.Find(department) ?? throw new Exception("Department not found");
            department.DeletedAt = DateTime.UtcNow;
            _context.Departments.Update(department);
            _context.SaveChanges();
        });
    }
    
    
}