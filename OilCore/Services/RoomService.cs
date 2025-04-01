using OilCore.Data;
using OilCore.Models;

namespace OilCore.Services;

public class RoomService
{
    private readonly OilCoreDbContext _context;
    private readonly IOilLoggerService<RoomService> _logger;
    
    public RoomService(OilCoreDbContext context, IOilLoggerService<RoomService> logger)
    {
        _context = context;
        _logger = logger;
        _logger.LogInfo("RoomService initialized");
    }
    
    public void Dispose()
    {
        // Dispose of the context if needed
        _logger.LogInfo("Disposing RoomService");
        _context?.Dispose();
    }   
    
    public List<Room> GetRooms()
    {
        _logger.LogInfo("Getting rooms");
        return _context.Rooms.Where(x => x.DeletedAt == null).ToList();
    }
    
    public Room GetRoom(Guid uid)
    {
        _logger.LogInfo("Getting room by ID");
        return _context.Rooms.Find(uid) ?? throw new Exception("Room not found");
    }
    
    public Room AddRoom(Room room)
    {
        _logger.LogInfo("Adding room");
        _context.Rooms.Add(room);
        _context.SaveChanges();
        return room;
    }
    
    public Room UpdateRoom(Room room)
    {
        _logger.LogInfo("Updating room");
        _context.Rooms.Update(room);
        _context.SaveChanges();
        return room;
    }
    
    public void DeleteRoom(Room room)
    {
        _logger.LogInfo("Deleting room");
        room = _context.Rooms.Find(room) ?? throw new Exception("Room not found");
        room.DeletedAt = DateTime.UtcNow;
        UpdateRoom(room);
    }
    
    public async Task<List<Room>> GetRoomsAsync()
    {
        _logger.LogInfo("Getting rooms asynchronously");
        return await Task.Run(GetRooms);
    }
    
    public async Task<Room> AddRoomAsync(Room room)
    {
        _logger.LogInfo("Adding room asynchronously");
        return await Task.Run(() => AddRoom(room));
    }
    
    public async Task<Room> UpdateRoomAsync(Room room)
    {
        _logger.LogInfo("Updating room asynchronously");
        return await Task.Run(() => UpdateRoom(room));
    }
    
    public async Task DeleteRoomAsync(Room room)
    {
        _logger.LogInfo("Deleting room asynchronously");
        await Task.Run(() => DeleteRoom(room));
    }
    
    // -- Classroom methods
    
    public List<Classroom> GetClassrooms()
    {
        _logger.LogInfo("Getting classrooms");
        return _context.Classrooms.Where(x => x.DeletedAt == null).ToList();
    }
    
    public Classroom GetClassroom(Guid uid)
    {
        _logger.LogInfo("Getting classroom by ID");
        return _context.Classrooms.Find(uid) ?? throw new Exception("Classroom not found");
    }
    
    public Classroom AddClassroom(Classroom classroom)
    {
        _logger.LogInfo("Adding classroom");
        _context.Classrooms.Add(classroom);
        _context.SaveChanges();
        return classroom;
    }
    
    public Classroom UpdateClassroom(Classroom classroom)
    {
        _logger.LogInfo("Updating classroom");
        _context.Classrooms.Update(classroom);
        _context.SaveChanges();
        return classroom;
    }
    
    public void DeleteClassroom(Classroom classroom)
    {
        _logger.LogInfo("Deleting classroom");
        classroom = _context.Classrooms.Find(classroom) ?? throw new Exception("Classroom not found");
        classroom.DeletedAt = DateTime.UtcNow;
        UpdateClassroom(classroom);
    }
    
    public async Task<Classroom> AddClassroomAsync(Classroom classroom)
    {
        _logger.LogInfo("Adding classroom asynchronously");
        return await Task.Run(() => AddClassroom(classroom));
    }
    
    public async Task<Classroom> UpdateClassroomAsync(Classroom classroom)
    {
        _logger.LogInfo("Updating classroom asynchronously");
        return await Task.Run(() => UpdateClassroom(classroom));
    }
    
    public async Task DeleteClassroomAsync(Classroom classroom)
    {
        _logger.LogInfo("Deleting classroom asynchronously");
        await Task.Run(() => DeleteClassroom(classroom));
    }
    
    // Office methods
    
    public List<Office> GetOffices()
    {
        _logger.LogInfo("Getting offices");
        return _context.Offices.Where(x => x.DeletedAt == null).ToList();
    }
    
    public Office GetOffice(Guid uid)
    {
        _logger.LogInfo("Getting office by ID");
        return _context.Offices.Find(uid) ?? throw new Exception("Office not found");
    }
    
    public Office AddOffice(Office office)
    {
        _logger.LogInfo("Adding office");
        _context.Offices.Add(office);
        _context.SaveChanges();
        return office;
    }
    
    public Office UpdateOffice(Office office)
    {
        _logger.LogInfo("Updating office");
        _context.Offices.Update(office);
        _context.SaveChanges();
        return office;
    }
    
    public void DeleteOffice(Office office)
    {
        _logger.LogInfo("Deleting office");
        office = _context.Offices.Find(office) ?? throw new Exception("Office not found");
        office.DeletedAt = DateTime.UtcNow;
        UpdateOffice(office);
    }
    
    public async Task<Office> AddOfficeAsync(Office office)
    {
        _logger.LogInfo("Adding office asynchronously");
        return await Task.Run(() => AddOffice(office));
    }
    
    public async Task<Office> UpdateOfficeAsync(Office office)
    {
        _logger.LogInfo("Updating office asynchronously");
        return await Task.Run(() => UpdateOffice(office));
    }
    
    public async Task DeleteOfficeAsync(Office office)
    {
        _logger.LogInfo("Deleting office asynchronously");
        await Task.Run(() => DeleteOffice(office));
    }
    
    // MeetingRoom methods
    
    public List<MeetingRoom> GetMeetingRooms()
    {
        _logger.LogInfo("Getting meeting rooms");
        return _context.MeetingRooms.Where(x => x.DeletedAt == null).ToList();
    }
    
    public MeetingRoom GetMeetingRoom(Guid uid)
    {
        _logger.LogInfo("Getting meeting room by ID");
        return _context.MeetingRooms.Find(uid) ?? throw new Exception("Meeting room not found");
    }
    
    public MeetingRoom AddMeetingRoom(MeetingRoom meetingRoom)
    {
        _logger.LogInfo("Adding meeting room");
        _context.MeetingRooms.Add(meetingRoom);
        _context.SaveChanges();
        return meetingRoom;
    }
    
    public MeetingRoom UpdateMeetingRoom(MeetingRoom meetingRoom)
    {
        _logger.LogInfo("Updating meeting room");
        _context.MeetingRooms.Update(meetingRoom);
        _context.SaveChanges();
        return meetingRoom;
    }
    
    public void DeleteMeetingRoom(MeetingRoom meetingRoom)
    {
        _logger.LogInfo("Deleting meeting room");
        meetingRoom = _context.MeetingRooms.Find(meetingRoom) ?? throw new Exception("Meeting room not found");
        meetingRoom.DeletedAt = DateTime.UtcNow;
        UpdateMeetingRoom(meetingRoom);
    }
    
    public async Task<MeetingRoom> AddMeetingRoomAsync(MeetingRoom meetingRoom)
    {
        _logger.LogInfo("Adding meeting room asynchronously");
        return await Task.Run(() => AddMeetingRoom(meetingRoom));
    }
    
    public async Task<MeetingRoom> UpdateMeetingRoomAsync(MeetingRoom meetingRoom)
    {
        _logger.LogInfo("Updating meeting room asynchronously");
        return await Task.Run(() => UpdateMeetingRoom(meetingRoom));
    }
    
    public async Task DeleteMeetingRoomAsync(MeetingRoom meetingRoom)
    {
        _logger.LogInfo("Deleting meeting room asynchronously");
        await Task.Run(() => _context.MeetingRooms.Remove(meetingRoom));
    }
    
    
        
}