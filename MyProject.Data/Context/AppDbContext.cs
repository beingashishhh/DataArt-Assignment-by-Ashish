using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Entities;

namespace MyProject.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Meeting> Meetings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Meeting>()
                .HasMany(m => m.Attendees)
                .WithMany(a => a.Meetings)
                .UsingEntity(j => j.ToTable("MeetingAttendees"));
        }
    }
}
