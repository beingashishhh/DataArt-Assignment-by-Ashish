using System.Text.Json.Serialization;

namespace MyProject.Domain.Entities
{
    public class Calendar
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [JsonIgnore]
        public List<Event> Events { get; set; } = new();
    }
}
