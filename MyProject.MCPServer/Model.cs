using System;
using System.Collections.Generic;

public class MCPRequest
{
    public string Method { get; set; } = "";
    public Dictionary<string, string>? Params { get; set; }
}

public class EventRecord
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public string Timezone { get; set; } = "UTC";
    public string? ClientReferenceId { get; set; }

    public object ToResponse()
    {
        return new
        {
            id = Id,
            title = Title,
            start = Start.ToString("o"),
            end = End.ToString("o"),
            timezone = Timezone
        };
    }
}

public static class ProgramState
{
    public static int NextId;
}
