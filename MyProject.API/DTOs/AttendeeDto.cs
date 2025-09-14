namespace MyProject.API.DTOs
{
    public class AttendeeDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? Name { get; set; }
        public int EventId { get; set; }
    }
}
