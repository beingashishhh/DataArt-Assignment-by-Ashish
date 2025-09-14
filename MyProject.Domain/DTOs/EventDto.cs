namespace MyProject.Domain.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }
        public string Timezone { get; set; } = "UTC";
        public string? RecurrenceRule { get; set; }
        public string? ClientReferenceId { get; set; }
        public int CalendarId { get; set; }
    }
}
