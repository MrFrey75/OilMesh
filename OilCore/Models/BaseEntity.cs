using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace OilCore.Models;

// ---- Base Classes ----
public abstract class BaseEntity
{
    [Key]
    public Guid Uid { get; init; } = Guid.NewGuid();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    private DateTime _updatedAt = DateTime.UtcNow;
    public DateTime UpdatedAt
    {
        get => _updatedAt;
        set => _updatedAt = value < CreatedAt ? CreatedAt : value;
    }

    public DateTime? DeletedAt { get; set; } // Soft delete
}

// ---- People ----
public class Person : BaseEntity
{
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();
}

public class User : Person
{
    [MaxLength(20)]
    public string Username { get; set; } = string.Empty;

    public virtual HashSet<Credential> Credentials { get; set; } = new();

    [NotMapped]
    public Credential? CurrentCredential => Credentials.OrderByDescending(c => c.CreatedAt).FirstOrDefault();
}

public class Credential : BaseEntity
{
    [MaxLength(200)]
    public string PasswordHash { get; set; } = string.Empty;

    [MaxLength(200)]
    public string PasswordSalt { get; set; } = string.Empty;
}

// ---- Specialized Users ----
public sealed class StudentUser : User
{
    public int GraduationYear { get; set; }
    public School School { get; set; } = new();
    public HashSet<Course> Courses { get; set; } = new();
}

public sealed class FacultyMember : User
{
    public Department Department { get; set; } = new();
    public HashSet<Course> Courses { get; set; } = new();
}

public sealed class Administrator : User
{
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Position { get; set; } = string.Empty;

    public HashSet<Office> Offices { get; set; } = new();
}

// ---- Education ----
public sealed class Department : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    public HashSet<FacultyMember> Faculty { get; set; } = new();
    public HashSet<Course> Courses { get; set; } = new();
}

public sealed class Course : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    public Department Department { get; set; } = new();

    public HashSet<FacultyMember> Instructors { get; set; } = new();
    public HashSet<StudentUser> Students { get; set; } = new();
    public HashSet<Classroom> Classrooms { get; set; } = new();
}

// ---- Contact Information ----
public class Address : BaseEntity
{
    public AddressType Type { get; set; } = AddressType.Unknown;

    [MaxLength(50)] public string Street { get; set; } = string.Empty;
    [MaxLength(50)] public string City { get; set; } = "Mt. Pleasant";
    [MaxLength(2)]  public string State { get; set; } = "MI";
    [MaxLength(10)] public string ZipCode { get; set; } = "48858";
    [MaxLength(10)] public string Country { get; set; } = "US";
}

public class PhoneNumber : BaseEntity
{
    public PhoneType Type { get; set; } = PhoneType.Unknown;

    [MaxLength(15)]
    public string Number { get; set; } = string.Empty;
}

public class EmailAddress : BaseEntity
{
    public EmailType Type { get; set; } = EmailType.Unknown;

    [MaxLength(50)]
    public string Address { get; set; } = string.Empty;
}

public enum AddressType { Unknown, Home, Work, School, Other }
public enum PhoneType { Unknown, Home, Work, Mobile, Fax, Other }
public enum EmailType { Unknown, Personal, Work, School, Other }

// ---- Schools & Buildings ----
public class School : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public virtual Address Address { get; set; } = new();
    public bool IsDefault { get; set; }
}

public class Room : BaseEntity
{
    [MaxLength(3)]
    public string Number { get; init; } = "000";
    public virtual HashSet<Asset> Assets { get; set; } = new();
}

public sealed class Classroom : Room
{
    public bool Primary { get; set; }
    public HashSet<Course> Courses { get; set; } = new();
}

public sealed class Office : Room
{
    [MaxLength(50)]
    public string OfficeName { get; set; } = string.Empty;

    public HashSet<Administrator> Administrators { get; set; } = new();
}

public class MeetingRoom : Room
{
    [MaxLength(50)]
    public string RoomName { get; set; } = string.Empty;
}

// ---- Assets ----

public class AssetModel : BaseEntity
{
    [MaxLength(50)]
    public string ModelName { get; set; } = string.Empty;

    [MaxLength(50)]
    public string ModelNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public Manufacturer Manufacturer { get; set; } = new();

    public AssetType Type { get; set; } = AssetType.Unknown;
    public HashSet<ConnectionType> Connections { get; set; } = [];

    public bool IsWifiCapable => Connections.Any(x => x == ConnectionType.WiFi);
    public bool IsBluetoothCapable => Connections.Any(x => x == ConnectionType.Bluetooth);
    public bool IsEthernetCapable => Connections.Any(x => x == ConnectionType.Ethernet);
    
    public HashSet<UsbType> UsbTypes { get; set; } = [];
}

public class Asset : BaseEntity
{
    [MaxLength(50)]
    public string AssetName { get; set; } = string.Empty;
    [MaxLength(15)]
    public string AssetNumber { get; set; } = string.Empty;

    [MaxLength(15)]
    public string SerialNumber { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    public AssetStatus Status { get; set; } = AssetStatus.Unknown;
    public AssetModel Model { get; set; } = new();
}


public class Manufacturer : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string Website { get; set; } = string.Empty;
}

public sealed class Workstation : Asset
{
    [MaxLength(15)]
    public string DeviceName
    {
        get => AssetName;
        set => AssetName = value;
    }

    public WorkstationType WorkstationType { get; set; } = WorkstationType.Unknown;
    public HashSet<Peripheral> Peripherals { get; set; } = [];
}

public class Printer : Asset
{
    [MaxLength(50)]
    public string PrinterName
    {
        get => AssetName;
        set => AssetName = value;
    }

    public PrinterType PrinterType { get; set; } = PrinterType.Unknown;
    public string SecurityPin { get; set; } = string.Empty;
}

public class Monitor : Peripheral
{
    public string MonitorName
    {
        get => PeripheralName;
        set => PeripheralName = value;
    }
    public MonitorType MonitorType { get; set; } = MonitorType.Unknown;
    public string Resolution { get; set; } = string.Empty;
}

public class Peripheral : Asset
{
    [MaxLength(50)]
    public string PeripheralName
    {
        get => AssetName;
        set => AssetName = value;
    }
    public PeripheralType PeripheralType { get; set; } = PeripheralType.Unknown;
}

// ---- Asset Related Enums ----
public enum PrinterType { Unknown, InkJet, Laser, DotMatrix, Thermal, Filament, Other }
public enum PeripheralType { Unknown, Monitor, Keyboard, Mouse, Scanner, Printer, Camera, Microphone, Speaker, Tablet, Other }
public enum AssetStatus { Unknown, Active, Inactive, Retired, Damaged, Repairing, Missing, Other }
public enum MonitorType { Unknown, CRT, LCD, Smart, Other }
public enum ConnectionType { Unknown, USB, Bluetooth, WiFi, Ethernet, HDMI, DisplayPort, Other }

public enum UsbType
{
    Unknown,        // Default
    USB_A,         // Standard rectangular USB Type-A
    USB_B,         // Square-shaped USB Type-B (printers, scanners)
    USB_MiniA,     // Mini-USB Type-A (legacy devices)
    USB_MiniB,     // Mini-USB Type-B (older phones, cameras)
    USB_MicroA,    // Micro-USB Type-A (rare, mostly obsolete)
    USB_MicroB,    // Micro-USB Type-B (older Android phones, accessories)
    USB_C,         // Reversible USB Type-C (modern standard)
    USB_3_A,       // USB 3.0 Type-A (blue port, faster speeds)
    USB_3_B,       // USB 3.0 Type-B (larger, for some external drives)
    USB_3_MicroB,  // USB 3.0 Micro-B (external hard drives)
    USB_4,         // Latest USB 4 standard (integrated with Thunderbolt 3)
    Thunderbolt3,  // Thunderbolt 3 (USB-C connector)
    Thunderbolt4   // Thunderbolt 4 (USB-C connector, more features)
}

public enum WorkstationType { Unknown, Desktop, Laptop, Surface, Server, Tablet, Other }
public enum AssetType { Unknown, Computer, Monitor, Printer, Scanner, Projector, Camera, Speaker, Microphone, Keyboard, Mouse, Tablet, Laptop, Server, Switch, Router, Firewall, AccessPoint, Storage, UPS, Other }
