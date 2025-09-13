using System.Text.Json.Serialization;

namespace MyProject.Domain.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [JsonIgnore]
        public List<Attendee> Attendees { get; set; } = new();
    }
}
