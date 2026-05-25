namespace EventEase.Models;

public class EventModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public string Location { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public static List<EventModel> GetSampleEvents()
    {
        return new List<EventModel>
        {
            new()
            {
                Id = 1,
                Name = "Annual Leadership Summit",
                Date = DateTime.Today.AddDays(14),
                Location = "Seattle, WA",
                Description = "A two-day summit with keynote sessions and breakout workshops."
            },
            new()
            {
                Id = 2,
                Name = "Product Launch Gala",
                Date = DateTime.Today.AddDays(28),
                Location = "Austin, TX",
                Description = "Celebrate the new product line with demos, speakers, and networking."
            },
            new()
            {
                Id = 3,
                Name = "Community Fundraiser",
                Date = DateTime.Today.AddDays(45),
                Location = "Chicago, IL",
                Description = "An evening of live entertainment and community engagement."
            }
        };
    }
}
