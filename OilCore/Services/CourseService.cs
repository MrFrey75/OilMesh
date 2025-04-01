using OilCore.Data;
using OilCore.Interfaces;
using OilCore.Models;

namespace OilCore.Services;

public class CourseService
{
    private readonly IOilLoggerService<CourseService> _logger;
    private readonly OilCoreDbContext _context;

    public CourseService(IOilLoggerService<CourseService> logger, OilCoreDbContext context)
    {
        _logger = logger;
        _context = context;
        _logger.LogInfo("CourseService initialized");
    }
    
    public void Dispose()
    {
        // Dispose of the context if needed
        _logger.LogInfo("Disposing CourseService");
        _context?.Dispose();
    }
    
    public List<Course> GetCourses()
    {
        _logger.LogInfo("Getting courses");
        return _context.Courses.Where(x => x.DeletedAt == null).ToList();
    }
    
    public Course GetCourse(Guid uid)
    {
        _logger.LogInfo("Getting course by ID");
        var course = _context.Courses.Find(uid) ?? throw new Exception("Course not found");
        return _context.Courses.Find(uid) ?? throw new Exception("Course not found");
    }
    
    public void AddCourse(Course course)
    {
        _logger.LogInfo("Adding course");
        _context.Courses.Add(course);
        _context.SaveChanges();
    }
    
    public void UpdateCourse(Course course)
    {
        _logger.LogInfo("Updating course");
        _context.Courses.Update(course);
        _context.SaveChanges();
    }
    
    public void DeleteCourse(Course course)
    {
        _logger.LogInfo("Deleting course");
        course = _context.Courses.Find(course) ?? throw new Exception("Course not found");
        course.DeletedAt = DateTime.UtcNow;
        _context.Courses.Update(course);
        _context.SaveChanges();
    }
    
    public async Task<Course> GetCourseAsync(Guid uid)
    {
        _logger.LogInfo("Getting course by ID asynchronously");
        var course = await _context.Courses.FindAsync(uid);
        if (course == null)
        {
            throw new Exception("Course not found");
        }
        return course;
    }
    
    public async Task AddCourseAsync(Course course)
    {
        _logger.LogInfo("Adding course asynchronously");
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateCourseAsync(Course course)
    {
        _logger.LogInfo("Updating course asynchronously");
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteCourseAsync(Course course)
    {
        _logger.LogInfo("Deleting course asynchronously");
        course = await _context.Courses.FindAsync(course) ?? throw new Exception("Course not found");
        course.DeletedAt = DateTime.UtcNow;
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }
}