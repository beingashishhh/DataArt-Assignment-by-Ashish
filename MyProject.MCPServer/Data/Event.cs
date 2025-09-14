namespace MyProject.MCPServer.Data
{
    public class Event
    {
        public int Id { get; set; }  
        public string Title { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int DurationMinutes { get; set; }
        public string AttendeesJson { get; set; } = "[]";
        public string Location { get; set; } = string.Empty;

       
        public string[] Attendees
        {
            get => System.Text.Json.JsonSerializer.Deserialize<string[]>(AttendeesJson) ?? Array.Empty<string>();
            set => AttendeesJson = System.Text.Json.JsonSerializer.Serialize(value);
        }
    }
}
