using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{
    public DbSet<Event> Events {get; set; }
    public DbSet<Attendee> Attendees { get; set; }
    public DbSet<User> Users { get; set; }
    public PassInDbContext(DbContextOptions<PassInDbContext> options) : base(options) { }
}
