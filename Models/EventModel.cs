using System.ComponentModel.DataAnnotations;
namespace EventEase.Models;

public class EventModel : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Event name is required.")]
    [StringLength(200, ErrorMessage = "Event name must be under 200 characters.")]
    public string Name { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Location is required.")]
    [StringLength(200)]
    public string Location { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Date < DateTime.Today)
        {
            yield return new ValidationResult("Event date cannot be in the past.", new[] { nameof(Date) });
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return new ValidationResult("Event name is required.", new[] { nameof(Name) });
        }

        if (string.IsNullOrWhiteSpace(Location))
        {
            yield return new ValidationResult("Location is required.", new[] { nameof(Location) });
        }
    }

    public static List<EventModel> GetSampleEvents(int count = 3)
    {
        var baseEvents = new List<EventModel>
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

        if (count <= baseEvents.Count)
            return baseEvents.Take(count).ToList();

        var list = new List<EventModel>(baseEvents);
        for (int i = baseEvents.Count + 1; i <= count; i++)
        {
            list.Add(new EventModel
            {
                Id = i,
                Name = $"Sample Event #{i}",
                Date = DateTime.Today.AddDays(i % 365),
                Location = $"City #{i % 100}",
                Description = $"Auto-generated sample event number {i}."
            });
        }

        return list;
    }
}
