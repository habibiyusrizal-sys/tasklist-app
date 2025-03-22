using API.Entitites;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<TaskRecord> Tasks { get; set; }
}
