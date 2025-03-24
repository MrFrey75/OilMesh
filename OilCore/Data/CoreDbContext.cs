using Microsoft.EntityFrameworkCore;
using OilCore.Models;

namespace OilCore.Data;

public class CoreDbContext : DbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options) : base(options) { }

}
