using Microsoft.EntityFrameworkCore;
using OilCore.Enumerations;
using OilCore.Models;
using OilCore.Models.Base;
using Monitor = OilCore.Models.Monitor;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace OilCore.Data;

public class OilCoreDbContext : DbContext
{
    public OilCoreDbContext(DbContextOptions<OilCoreDbContext> options) : base(options) { }

    // App Config
    public DbSet<AppConfigModel> AppConfigs { get; set; }

    // Assets & Models
    public DbSet<Asset> Assets { get; set; }
    public DbSet<BaseAsset> BaseAssets { get; set; }
    public DbSet<Peripheral> Peripherals { get; set; }
    public DbSet<Monitor> Monitors { get; set; }
    public DbSet<Workstation> Workstations { get; set; }
    public DbSet<Printer> Printers { get; set; }

    // People & Users
    public DbSet<Person> People { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<StudentUser> StudentUsers { get; set; }
    public DbSet<FacultyUser> FacultyUsers { get; set; }
    public DbSet<AdministratorUser> AdministratorUsers { get; set; }

    // Credentials
    public DbSet<UserCredential> UserCredentials { get; set; }

    // Contact Info
    public DbSet<EmailAddress> EmailAddresses { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }

    // Addresses
    public DbSet<Address> Addresses { get; set; }

    // Physical Spaces
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<MeetingRoom> MeetingRooms { get; set; }
    public DbSet<AuxiliaryRoom> AuxiliaryRooms { get; set; }

    // Academic
    public DbSet<School> Schools { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Department> Departments { get; set; }

    // Manufacturers
    public DbSet<Manufacturer> Manufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Room and Assets Relationship
        modelBuilder.Entity<RoomModel>()
            .HasMany<AssetModel>(r => r.Assets)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DepartmentModel>()
            .HasMany<CourseModel>(d => d.Courses)
            .WithOne(c => c.Department)
            .OnDelete(DeleteBehavior.Cascade);

        // Many-to-Many Relationships
        modelBuilder.Entity<StudentUser>()
            .HasMany<CourseModel>(s => s.Courses)
            .WithMany(c => c.Students);

        modelBuilder.Entity<FacultyUser>()
            .HasMany<CourseModel>(f => f.Courses)
            .WithMany(c => c.FacultyMembers);

        modelBuilder.Entity<AdministratorUser>()
            .HasMany(a => a.Offices)
            .WithMany(o => o.Administrators);

        // School and Address Relationship
        modelBuilder.Entity<SchoolModel>()
            .HasOne(s => s.Address)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        // User and Credentials Relationship (Fixing the protected property issue)
        modelBuilder.Entity<User>()
            .HasMany<UserCredentialModel>("Credentials") // Explicitly specify the navigation property
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Cascade);



    }

}
