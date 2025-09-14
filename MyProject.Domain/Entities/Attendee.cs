using System.Text.Json.Serialization;

namespace MyProject.Domain.Entities
{
    public class Attendee
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }

        public int EventId { get; set; }

        [JsonIgnore]
        public Event? Event { get; set; }

        [JsonIgnore]
        public List<Meeting> Meetings { get; set; } = new();
    }
}
