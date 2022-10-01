using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Challenge> Challenges { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Participant> Participants { get; set; }
}
