using System.Text.Json.Serialization;

namespace MyProject.Domain.Entities
{
    public class Meeting
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        [JsonIgnore]
        public List<Attendee> Attendees { get; set; } = new();
    }
}
