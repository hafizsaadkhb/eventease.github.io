namespace EventEase.Models;

public class RegistrationRecord
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime RegisteredAtUtc { get; set; } = DateTime.UtcNow;
    public bool Attended { get; set; } = false;
}
